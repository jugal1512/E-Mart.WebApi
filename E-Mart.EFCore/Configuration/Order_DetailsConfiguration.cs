using E_Mart.Domain.OrderDetails;
using E_Mart.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class Order_DetailsConfiguration : IEntityTypeConfiguration<Order_Details>
{
    public void Configure(EntityTypeBuilder<Order_Details> builder)
    {
        builder.ToTable("Order_Details");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.UserId).IsRequired();
        builder.Property(o => o.TotalAmount).IsRequired();
        builder.Property(o => o.CreatedAt).IsRequired();
        builder.Property(o => o.UpdatedAt);

        builder
            .HasOne(od => od.Payment_Details)
            .WithOne(pd => pd.OrderDetails)
            .HasForeignKey<Payment_Details>(od => od.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
