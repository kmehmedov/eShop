using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Order.Domain;

namespace Order.Infrastructure
{
    public class OrderContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "order";

        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        public DbSet<Domain.Models.Orders.Order> Orders { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);
        }
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>()
                .UseSqlServer("Server=tcp:127.0.0.1,1433;Initial Catalog=OrderDb;User Id=sa;Password=Passw0rd;Encrypt=false");

            return new OrderContext(optionsBuilder.Options);
        }
    }
}
