using E_Mart.Domain.Sellers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Configuration;
public class SellerOrdersConfiguration : IEntityTypeConfiguration<SellerOrders>
{
    public void Configure(EntityTypeBuilder<SellerOrders> builder)
    {
        builder.ToTable("SellerOrders");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.SellerId).IsRequired();
        builder.Property(x => x.TotalAmount).IsRequired().HasColumnType("decimal(7,2)");
        builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);

        builder.Ignore(x => x.IsActive);

        builder
            .HasOne(so => so.OrderDetails)
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(so => so.Seller)
            .WithMany()
            .HasForeignKey(so => so.SellerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}