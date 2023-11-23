using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infrastructure.EntityTypeConfigurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<Order.Domain.Models.Orders.OrderItem>
    {
        public void Configure(EntityTypeBuilder<Order.Domain.Models.Orders.OrderItem> builder)
        {
            builder.ToTable("OrderItem", OrderContext.DEFAULT_SCHEMA);

            builder.Property(ci => ci.Id)
                .UseHiLo("order_item_hilo")
                .IsRequired();

            builder.Property(oi => oi.ProductId)
                .IsRequired();

            builder.Property(oi => oi.ProductName)
                .IsRequired();

            builder.Property(oi => oi.UnitQuantity)
                .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                .HasColumnType("decimal")
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
