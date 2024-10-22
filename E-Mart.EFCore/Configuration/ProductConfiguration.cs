using E_Mart.Domain.Products;
using E_Mart.Domain.Sellers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        builder.Property(p => p.ProductName).IsRequired();
        builder.Property(p => p.ProductDescription).IsRequired().HasMaxLength(500);
        builder.Property(p => p.OriginalPrice).IsRequired();
        builder.Property(p => p.ActualPrice).IsRequired();
        builder.Property(p => p.Stock).IsRequired();
        builder.Property(p => p.CategoryId).IsRequired();
        builder.Property(p => p.SellerId).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.Property(p => p.UpdatedAt);
        builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(p => p.CreatedBy).IsRequired();
        builder.Property(p => p.UpdatedBy);
        builder.Property(c => c.IsDeleted).HasDefaultValue(false);

        builder
            .HasOne(p => p.SubCategories)
            .WithMany(sc => sc.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<Seller>()
            .WithMany()
            .HasForeignKey(p => p.SellerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
