using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Models.CatalogItems;
using Catalog.Domain.Models.CatalogBrands;

namespace Catalog.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "catalog";

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
                .UseSqlServer("Server=tcp:127.0.0.1,1433;Initial Catalog=CatalogDb;User Id=sa;Password=Passw0rd;Encrypt=false");

            return new CatalogContext(optionsBuilder.Options);
        }
    }
}
