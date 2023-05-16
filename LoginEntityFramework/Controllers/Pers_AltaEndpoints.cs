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

        routes.MapPut("/api/Pers_Alta/update/{Id:int}", async (int Id, Pers_Portal_Servicios_Acta_Instalacion_Comercial pers_Alta, LoginEntityFrameworkContext db) =>
        {
            var foundModel = await db.Pers_Portal_Servicios_Acta_Instalacion_Comercial.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            // Actualizar el objeto encontrado con los nuevos datos
            foundModel.IdCliente = pers_Alta.IdCliente;
            foundModel.IdProyecto = pers_Alta.IdProyecto;
            foundModel.IdComercial = pers_Alta.IdComercial;
            foundModel.Mantenimiento = pers_Alta.Mantenimiento;
            foundModel.Puestos = pers_Alta.Puestos;
            foundModel.SO = pers_Alta.SO;
            foundModel.ConexionRed = pers_Alta.ConexionRed;
            foundModel.RetiradaMaquina = pers_Alta.RetiradaMaquina;
            foundModel.MaquinaRetirada = pers_Alta.MaquinaRetirada;
            foundModel.Contadores = pers_Alta.Contadores;
            foundModel.DireccionEnvio = pers_Alta.DireccionEnvio;
            foundModel.Contacto = pers_Alta.Contacto;
            foundModel.Telefono = pers_Alta.Telefono;
            foundModel.Horario = pers_Alta.Horario;
            foundModel.Observaciones = pers_Alta.Observaciones;
            // ...

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdatePers_Alta")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Pers_Alta/add", async (Pers_Portal_Servicios_Acta_Instalacion_Comercial pers_Alta, LoginEntityFrameworkContext db) =>
        {
            pers_Alta.fecha = DateTime.Now;

            db.Pers_Portal_Servicios_Acta_Instalacion_Comercial.Add(pers_Alta);
            await db.SaveChangesAsync();
            return Results.Created($"/Pers_Altas/{pers_Alta.Id}", pers_Alta);
        })
        .WithName("CreatePers_Alta")
        .Produces<Pers_Portal_Servicios_Acta_Instalacion_Comercial>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Pers_Alta/delete/{Id:int}", async (int Id, LoginEntityFrameworkContext db) =>
        {
            if (await db.Pers_Portal_Servicios_Acta_Instalacion_Comercial.FindAsync(Id) is Pers_Portal_Servicios_Acta_Instalacion_Comercial pers_Alta)
            {
                db.Pers_Portal_Servicios_Acta_Instalacion_Comercial.Remove(pers_Alta);
                await db.SaveChangesAsync();
                return Results.Ok("Dato eliminado!");
            }

            return Results.NotFound();
        })
        .WithName("DeletePers_Alta")
        .Produces<Pers_Portal_Servicios_Acta_Instalacion_Comercial>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
