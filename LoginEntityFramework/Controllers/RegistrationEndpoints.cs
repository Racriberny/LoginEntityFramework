using LoginEntityFramework.Data;
using LoginEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LoginEntityFramework.Controllers
{

public static class RegistrationEndpoints
{
	public static void MapRegistrationEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Registration", async (LoginEntityFrameworkContext db) =>
        {
            return await db.Registration.ToListAsync();
        })
        .WithName("GetAllRegistrations")
        .Produces<List<Registration>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Registration/{id}", async (int Id, LoginEntityFrameworkContext db) =>
        {
            return await db.Registration.FindAsync(Id)
                is Registration model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetRegistrationById")
        .Produces<Registration>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Registration/{id}", async (int Id, Registration registration, LoginEntityFrameworkContext db) =>
        {
            var foundModel = await db.Registration.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(registration);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateRegistration")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Registration/", async (Registration registration, LoginEntityFrameworkContext db) =>
        {
            var tokenString = GenerateJwtToken(registration.Id, registration.Email);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(registration.Password);
            byte[] hashBytes;
            using (SHA256 sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(passwordBytes);
            }
            string passwordHash = Convert.ToBase64String(hashBytes);
            registration.Password = passwordHash;
            registration.Token = tokenString;
            db.Registration.Add(registration);
            await db.SaveChangesAsync();
            return Results.Created($"/Registrations/{registration.Id}", registration);
        })
        .WithName("CreateRegistration")
        .Produces<Registration>(StatusCodes.Status201Created);


        routes.MapDelete("/api/Registration/{id}", async (int Id, LoginEntityFrameworkContext db) =>
        {
            if (await db.Registration.FindAsync(Id) is Registration registration)
            {
                db.Registration.Remove(registration);
                await db.SaveChangesAsync();
                return Results.Ok(registration);
            }

            return Results.NotFound();
        })
        .WithName("DeleteRegistration")
        .Produces<Registration>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }

        private static string GenerateJwtToken(int userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("clave-secreta-para-generar-el-token"); // Reemplaza esto con tu clave secreta
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim("userId", userId.ToString()),
            new Claim(ClaimTypes.Email, email)
                    // Agrega cualquier otra reclamación personalizada que desees incluir en el token
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Limitar la longitud del token a 120 caracteres
            if (tokenString.Length > 120)
            {
                tokenString = tokenString.Substring(0, 120);
            }

            return tokenString;
        }
    }
}
