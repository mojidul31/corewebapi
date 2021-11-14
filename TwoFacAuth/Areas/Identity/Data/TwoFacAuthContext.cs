using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwoFacAuth.Areas.Identity.Data;

namespace TwoFacAuth.Data
{
    public class TwoFacAuthContext : IdentityDbContext<TwoFacAuthUser>
    {
        public TwoFacAuthContext() { }
        public TwoFacAuthContext(DbContextOptions<TwoFacAuthContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;user=sa;password=Admin_123;Database=CoreApi;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
