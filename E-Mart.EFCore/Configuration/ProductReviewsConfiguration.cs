using E_Mart.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class ProductReviewsConfiguration : IEntityTypeConfiguration<ProductReviews>
{
    public void Configure(EntityTypeBuilder<ProductReviews> builder)
    {
        builder.ToTable("ProductReviews");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Rating).IsRequired();
        builder.Property(x => x.Comment).IsRequired().HasMaxLength(500);
        builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.UpdatedAt);

        builder.Ignore(x => x.IsActive);

        builder
            .HasOne(pr => pr.User)
            .WithMany()
            .HasForeignKey(pr => pr.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(pr => pr.Product)
            .WithMany()
            .HasForeignKey(pr => pr.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}