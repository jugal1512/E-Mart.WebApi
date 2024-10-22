using E_Mart.Domain.Coupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class CouponsConfiguration : IEntityTypeConfiguration<Coupons>
{
    public void Configure(EntityTypeBuilder<Coupons> builder)
    {
        builder.ToTable("Coupons");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code);
        builder.Property(x => x.DiscountType);
        builder.Property(x => x.DiscountAmount);
        builder.Property(x => x.StartDate);
        builder.Property(x => x.EndDate);
        builder.Property(x => x.IsActive);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);

        builder.Ignore(x => x.IsDeleted);
    }
}