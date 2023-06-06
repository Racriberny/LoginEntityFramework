using Microsoft.EntityFrameworkCore;
using LoginEntityFramework.Data;
using LoginEntityFramework.Models;
namespace LoginEntityFramework.Controllers;

public static class Acta_InstalacionEndpoints
{
    public static void MapActa_InstalacionEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Acta_Instalacion/all", async (LoginEntityFrameworkContext db) =>
        {
            return await db.Acta_Instalacion.ToListAsync();
        })
        .WithName("GetAllActa_Instalacions")
        .Produces<List<Acta_Instalacion>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Acta_Instalacion/{id}", async (int Id, LoginEntityFrameworkContext db) =>
        {
            return await db.Acta_Instalacion.FindAsync(Id)
                is Acta_Instalacion model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetActa_InstalacionById")
        .Produces<Acta_Instalacion>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Acta_Instalacion/{id}", async (int Id, Acta_Instalacion acta_Instalacion, LoginEntityFrameworkContext db) =>
        {
            var foundModel = await db.Acta_Instalacion.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(acta_Instalacion);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateActa_Instalacion")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Acta_Instalacion/add", async (Acta_Instalacion acta_Instalacion, LoginEntityFrameworkContext db) =>
        {
            db.Acta_Instalacion.Add(acta_Instalacion);
            await db.SaveChangesAsync();
            return Results.Created($"/Acta_Instalacions/{acta_Instalacion.Id}", acta_Instalacion);
        })
        .WithName("CreateActa_Instalacion")
        .Produces<Acta_Instalacion>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Acta_Instalacion/delete/{Id:int}", async (int Id, LoginEntityFrameworkContext db) =>
        {
            if (await db.Acta_Instalacion.FindAsync(Id) is Acta_Instalacion acta_Instalacion)
            {
                db.Acta_Instalacion.Remove(acta_Instalacion);
                await db.SaveChangesAsync();
                return Results.Ok(acta_Instalacion);
            }

            return Results.NotFound();
        })
        .WithName("DeleteActa_Instalacion")
        .Produces<Acta_Instalacion>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
