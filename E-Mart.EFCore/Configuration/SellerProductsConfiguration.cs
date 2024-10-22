using E_Mart.Domain.Sellers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Configuration;
public class SellerProductsConfiguration : IEntityTypeConfiguration<SellerProducts>
{
    public void Configure(EntityTypeBuilder<SellerProducts> builder)
    {
        builder.ToTable("SellerProducts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SellerId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder
            .HasOne(s => s.Seller)
            .WithMany()
            .HasForeignKey(s => s.SellerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(s => s.Product)
            .WithMany()
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
