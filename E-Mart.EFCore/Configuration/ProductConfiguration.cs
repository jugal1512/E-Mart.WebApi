using E_Mart.Domain.Products;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Configuration;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        builder.Property(p => p.ProductName).IsRequired();
        builder.Property(p => p.ProductDescription).IsRequired().HasMaxLength(250);
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.Stock).IsRequired();
        builder.Property(p => p.CategoryId).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
        builder.Property(p => p.IsActive).IsRequired();
        builder.Property(p => p.ProductImage).IsRequired();
        builder.Property(p => p.CreatedBy).IsRequired();
        builder.Property(p => p.UpdatedBy);
        
        builder
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
