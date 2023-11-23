using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infrastructure.EntityTypeConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order.Domain.Models.Orders.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Orders.Order> builder)
        {
            builder.ToTable("Order", OrderContext.DEFAULT_SCHEMA);

            builder.Property(ci => ci.Id)
                .UseHiLo("order_hilo")
                .IsRequired();

            builder.Property(ci => ci.BuyerId)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Date)
                .IsRequired(true);
        }
    }
}
