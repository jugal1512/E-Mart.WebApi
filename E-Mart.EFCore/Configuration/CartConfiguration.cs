using E_Mart.Domain.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.Total).IsRequired().HasColumnType("decimal(7,2)");
        builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.Property(c => c.UpdatedAt);
        builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);

        builder
            .HasMany(c => c.CartItems)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}