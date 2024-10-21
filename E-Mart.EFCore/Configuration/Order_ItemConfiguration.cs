using E_Mart.Domain.OrderDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class Order_ItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.orderId).IsRequired();
        builder.Property(o => o.ProductId).IsRequired();
        builder.Property(o => o.Quantity).IsRequired();

        builder
            .HasOne(oi => oi.OrderDetails)
            .WithMany(od => od.OrderItems)
            .HasForeignKey(oi => oi.orderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
