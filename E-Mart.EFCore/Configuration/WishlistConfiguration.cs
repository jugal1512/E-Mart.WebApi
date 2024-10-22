using E_Mart.Domain.Wishlists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class WishlistConfiguration : IEntityTypeConfiguration<wishlist>
{
    public void Configure(EntityTypeBuilder<wishlist> builder)
    {
        builder.ToTable("Wishlists");
        builder.HasKey(w => w.Id);
        builder.Property(w => w.ProductId).IsRequired();
        builder.Property(w => w.UserId).IsRequired();
        builder.Property(w => w.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.Property(w => w.DeletedAt);
    }
}
