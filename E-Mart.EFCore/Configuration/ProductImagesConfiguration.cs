using E_Mart.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class ProductImagesConfiguration : IEntityTypeConfiguration<ProductImages>
{
    public void Configure(EntityTypeBuilder<ProductImages> builder)
    {
        builder.ToTable("ProductImages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductId);
        builder.Property(x => x.ImageUrl);
        builder.Property(x => x.IsPrimary);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);

        builder.Ignore(x => x.IsActive);
        builder.Ignore(x => x.IsDeleted);

        builder
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}