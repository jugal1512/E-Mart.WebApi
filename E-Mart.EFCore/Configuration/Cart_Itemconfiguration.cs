using E_Mart.Domain.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Configuration;
public class Cart_Itemconfiguration : IEntityTypeConfiguration<Cart_Item>
{
    public void Configure(EntityTypeBuilder<Cart_Item> builder)
    {
        builder.ToTable("Cart_Items");
        builder.HasKey(ci => ci.Id);
        builder.Property(ci => ci.CartId).IsRequired();
        builder.Property(ci => ci.ProductId).IsRequired();
        builder.Property(ci => ci.Quantity).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt);
    }
}
