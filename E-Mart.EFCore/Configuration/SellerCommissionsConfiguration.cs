using E_Mart.Domain.Sellers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class SellerCommissionsConfiguration : IEntityTypeConfiguration<SellerCommissions>
{
    public void Configure(EntityTypeBuilder<SellerCommissions> builder)
    {
        builder.ToTable("SellerCommission");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SellerId).IsRequired();
        builder.Property(x => x.CommissionRate).IsRequired();
        builder.Property(x => x.EffectiveDate).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.Ignore(x => x.IsActive);

        builder
            .HasOne(sp => sp.Seller)
            .WithMany()
            .HasForeignKey(x => x.SellerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}