using Microsoft.EntityFrameworkCore;
using LoginEntityFramework.Models;

namespace LoginEntityFramework.Data
{
    public class LoginEntityFrameworkContext : DbContext
    {
        public LoginEntityFrameworkContext (DbContextOptions<LoginEntityFrameworkContext> options)
            : base(options)
        {
        }
        public DbSet<LoginEntityFramework.Models.Usuarios> Usuarios { get; set; } = default!;

        public DbSet<LoginEntityFramework.Models.Pers_Portal_Servicios_Acta_Instalacion_Comercial>? Pers_Portal_Servicios_Acta_Instalacion_Comercial { get; set; }

        public DbSet<LoginEntityFramework.Models.Acta_Instalacion>? Acta_Instalacion { get; set; }
    }
}
