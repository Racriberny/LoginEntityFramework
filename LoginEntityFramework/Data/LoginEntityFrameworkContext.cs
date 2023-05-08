﻿using Microsoft.EntityFrameworkCore;

namespace LoginEntityFramework.Data
{
    public class LoginEntityFrameworkContext : DbContext
    {
        public LoginEntityFrameworkContext (DbContextOptions<LoginEntityFrameworkContext> options)
            : base(options)
        {
        }

        public DbSet<LoginEntityFramework.Models.Registration> Registration { get; set; } = default!;
    }
}