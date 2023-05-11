using Microsoft.EntityFrameworkCore;
using LoginEntityFramework.Data;
using LoginEntityFramework.Models;
namespace LoginEntityFramework.Controllers;

public static class Pers_AltaEndpoints
{
    public static void MapPers_AltaEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Pers_Alta/all", async (LoginEntityFrameworkContext db) =>
        {
            return await db.Pers_Portal_Servicios_Acta_Instalacion_Comercial.ToListAsync();
        })
        .WithName("GetAllPers_Altas")
        .Produces<List<Pers_Portal_Servicios_Acta_Instalacion_Comercial>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Pers_Alta/{id}", async (int Id, LoginEntityFrameworkContext db) =>
        {
            return await db.Pers_Portal_Servicios_Acta_Instalacion_Comercial.FindAsync(Id)
                is Pers_Portal_Servicios_Acta_Instalacion_Comercial model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetPers_AltaById")
        .Produces<Pers_Portal_Servicios_Acta_Instalacion_Comercial>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Pers_Alta/{id}", async (int Id, Pers_Portal_Servicios_Acta_Instalacion_Comercial pers_Alta, LoginEntityFrameworkContext db) =>
        {
            var foundModel = await db.Pers_Portal_Servicios_Acta_Instalacion_Comercial.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(pers_Alta);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdatePers_Alta")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Pers_Alta/add", async (Pers_Portal_Servicios_Acta_Instalacion_Comercial pers_Alta, LoginEntityFrameworkContext db) =>
        {
            db.Pers_Portal_Servicios_Acta_Instalacion_Comercial.Add(pers_Alta);
            await db.SaveChangesAsync();
            return Results.Created($"/Pers_Altas/{pers_Alta.Id}", pers_Alta);
        })
        .WithName("CreatePers_Alta")
        .Produces<Pers_Portal_Servicios_Acta_Instalacion_Comercial>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Pers_Alta/{id}", async (int Id, LoginEntityFrameworkContext db) =>
        {
            if (await db.Pers_Portal_Servicios_Acta_Instalacion_Comercial.FindAsync(Id) is Pers_Portal_Servicios_Acta_Instalacion_Comercial pers_Alta)
            {
                db.Pers_Portal_Servicios_Acta_Instalacion_Comercial.Remove(pers_Alta);
                await db.SaveChangesAsync();
                return Results.Ok(pers_Alta);
            }

            return Results.NotFound();
        })
        .WithName("DeletePers_Alta")
        .Produces<Pers_Portal_Servicios_Acta_Instalacion_Comercial>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
