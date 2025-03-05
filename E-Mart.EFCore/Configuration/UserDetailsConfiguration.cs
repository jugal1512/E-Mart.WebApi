using E_Mart.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
{
    public void Configure(EntityTypeBuilder<UserDetails> builder)
    {
        builder.ToTable("UserDetails");
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.UserId);
        builder.Property(ua => ua.Address_line1).IsRequired().HasMaxLength(500);
        builder.Property(ua => ua.Address_line2).HasMaxLength(500);
        builder.Property(ua => ua.City).IsRequired().HasMaxLength(100);
        builder.Property(ua => ua.PostalCode).IsRequired();
        builder.Property(ua => ua.Country).IsRequired();
        builder.Property(ua => ua.Mobile).IsRequired();
        builder.Property(ua => ua.IsDefault);
    }
}