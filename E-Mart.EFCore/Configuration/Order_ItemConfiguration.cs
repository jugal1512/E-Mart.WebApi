using E_Mart.Domain.OrderDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        builder.Property(o => o.CreatedAt).IsRequired();
        builder.Property(o => o.UpdatedAt);

        builder
            .HasOne(oi => oi.OrderDetails)
            .WithMany(od => od.OrderItems)
            .HasForeignKey(oi => oi.orderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
