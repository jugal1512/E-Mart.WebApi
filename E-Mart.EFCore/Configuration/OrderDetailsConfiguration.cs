using E_Mart.Domain.OrderDetails;
using E_Mart.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.ToTable("OrderDetails");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.UserId).IsRequired();
        builder.Property(o => o.TotalAmount).IsRequired();
        builder.Property(o => o.CreatedAt).IsRequired();
        builder.Property(o => o.UpdatedAt);

        builder
            .HasOne(od => od.PaymentDetails)
            .WithOne(pd => pd.OrderDetails)
            .HasForeignKey<PaymentDetails>(od => od.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
