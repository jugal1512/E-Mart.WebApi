﻿using E_Mart.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class PaymentDetailsConfiguration : IEntityTypeConfiguration<PaymentDetails>
{
    public void Configure(EntityTypeBuilder<PaymentDetails> builder)
    {
        builder.ToTable("PaymentDetails");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.OrderId).IsRequired();
        builder.Property(p => p.Amount).IsRequired();
        builder.Property(p => p.Provider).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
    }
}