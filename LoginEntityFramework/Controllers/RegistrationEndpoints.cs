using LoginEntityFramework.Data;
using LoginEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
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
        routes.MapGet("/api/Registration/all", async (LoginEntityFrameworkContext db) =>
        {
            return await db.Usuarios.ToListAsync();
        })
        .WithName("GetAllRegistrations")
        .Produces<List<Usuarios>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Registration/{id}", async (int Id, LoginEntityFrameworkContext db) =>
        {
            return await db.Usuarios.FindAsync(Id)
                is Usuarios model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetRegistrationById")
        .Produces<Usuarios>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Registration/{id}", async (int Id, Usuarios registration, LoginEntityFrameworkContext db) =>
        {
            var foundModel = await db.Usuarios.FindAsync(Id);

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

        routes.MapPost("/api/Registration/registration", async (Usuarios registration, LoginEntityFrameworkContext db) =>
        {
            var tokenString = GenerateJwtToken(registration.Id, registration.Correo);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(registration.Passwd);
            byte[] hashBytes;
            using (SHA256 sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(passwordBytes);
            }
            string passwordHash = Convert.ToBase64String(hashBytes);
            registration.Passwd = passwordHash;
            registration.Token = tokenString;
            db.Usuarios.Add(registration);
            await db.SaveChangesAsync();
            return Results.Ok("Usuario Creado");
        })
        .WithName("CreateRegistration")
        .Produces<Usuarios>(StatusCodes.Status201Created);
            routes.MapDelete("/api/Registration/delete/{Id:int}", async (int Id, LoginEntityFrameworkContext db) =>
            {
                if (await db.Usuarios.FindAsync(Id) is Usuarios registration)
                {
                    db.Usuarios.Remove(registration);
                    await db.SaveChangesAsync();
                    return Results.Ok("Usuario Eliminado Correctamente ");
                }

                return Results.NotFound();
            })
            .WithName("DeleteRegistration")
            .Produces<Usuarios>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
            routes.MapPost("/api/Login", async (RegistrationLogin request, LoginEntityFrameworkContext db) =>
            {
                var foundUser = await db.Usuarios.FirstOrDefaultAsync(u => u.Correo == request.Email);

                if (foundUser == null)
                {
                    return Results.NotFound();
                }

                byte[] passwordBytes = Encoding.UTF8.GetBytes(request.Password);
                byte[] hashBytes;
                using (SHA256 sha256 = SHA256.Create())
                {
                    hashBytes = sha256.ComputeHash(passwordBytes);
                }
                string passwordHash = Convert.ToBase64String(hashBytes);

                if (request.Password == foundUser.Passwd)
                {
                    return Results.Ok("Login Correcto");
                }

                if (foundUser.Token == request.Token)
                {
                    return Results.Ok("Login correcto");
                }
                else
                {
                    return Results.Unauthorized();
                }

            })
            .WithName("Login")
            .Produces<Usuarios>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);


            }

        private static string GenerateJwtToken(int userId, string email)
        {
            var tokenHandlerr = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("clave-secreta-para-generar-el-token");
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
            var token = tokenHandlerr.CreateToken(tokenDescriptor);
            var tokenString = tokenHandlerr.WriteToken(token);
            var tokenSinEspacio = tokenString.Trim();
            return tokenSinEspacio;
        }
    }
}
