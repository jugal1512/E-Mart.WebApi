using E_Mart.Domain.Sellers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class SellerReviewsConfiguration : IEntityTypeConfiguration<SellerReviews>
{
    public void Configure(EntityTypeBuilder<SellerReviews> builder)
    {
        builder.ToTable("SellerReviews");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SellerId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Rating).IsRequired();
        builder.Property(x => x.Comment).IsRequired().HasMaxLength(500);
        builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.Ignore(x => x.IsActive);

        builder
            .HasOne(sr => sr.Seller)
            .WithMany()
            .HasForeignKey(sr => sr.SellerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(sr => sr.User)
            .WithMany()
            .HasForeignKey(sr => sr.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}