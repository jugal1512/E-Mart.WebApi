using E_Mart.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Configuration;
public class UserAddressesConfiguration : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.ToTable("UserAddresses");
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.UserId);
        builder.Property(ua => ua.Address_line1).IsRequired().HasMaxLength(250);
        builder.Property(ua => ua.Address_line2).HasMaxLength(250);
        builder.Property(ua => ua.City).IsRequired().HasMaxLength(100);
        builder.Property(ua => ua.PostalCode).IsRequired();
        builder.Property(ua => ua.Country).IsRequired();
        builder.Property(ua => ua.Mobile).IsRequired();
    }
}
