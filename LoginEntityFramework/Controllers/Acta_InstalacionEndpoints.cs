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

            foundModel.IdTecnico = acta_Instalacion.IdTecnico;
            foundModel.IdParte = acta_Instalacion.IdParte;
            foundModel.Modelo = acta_Instalacion.Modelo;
            foundModel.Serie = acta_Instalacion.Serie;
            foundModel.IdContacto = acta_Instalacion.IdContacto;
            foundModel.Telefono = acta_Instalacion.Telefono;
            foundModel.Email = acta_Instalacion.Email;
            foundModel.DireccionIp = acta_Instalacion.DireccionIp;
            foundModel.IdTipo = acta_Instalacion.IdTipo;
            foundModel.Ubicacion = acta_Instalacion.Ubicacion;
            foundModel.CajaToner = acta_Instalacion.CajaToner;
            foundModel.Adhesivo = acta_Instalacion.Adhesivo;
            foundModel.Retirar = acta_Instalacion.Retirar;
            foundModel.Marca = acta_Instalacion.Marca;
            foundModel.ModeloMaquina = acta_Instalacion.ModeloMaquina;
            foundModel.SerieMaquina = acta_Instalacion.SerieMaquina;
            foundModel.N_Copias = acta_Instalacion.N_Copias;
            foundModel.B_N = acta_Instalacion.B_N;
            foundModel.CL = acta_Instalacion.CL;
            foundModel.Observaciones = acta_Instalacion.Observaciones;

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
