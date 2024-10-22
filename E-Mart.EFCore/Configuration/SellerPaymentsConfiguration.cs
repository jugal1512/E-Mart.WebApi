using E_Mart.Domain.Sellers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class SellerPaymentsConfiguration : IEntityTypeConfiguration<SellerPayments>
{
    public void Configure(EntityTypeBuilder<SellerPayments> builder)
    {
        builder.ToTable("SellerPayments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SellerId).IsRequired();
        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(7,2)");
        builder.Property(x => x.PaymentDate).IsRequired();
        builder.Property(x => x.Status).IsRequired();
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