using E_Mart.Domain.Inventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductId);
        builder.Property(x => x.QuantityAvailable);
        builder.Property(x => x.QuantityReserved);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);

        builder.Ignore(x => x.IsActive);
        builder.Ignore(x => x.IsDeleted);

        builder
            .HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}