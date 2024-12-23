﻿using E_Mart.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.CategoryName).IsRequired();
        builder.Property(c => c.Description).IsRequired().HasMaxLength(250);
        builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()"); 
        builder.Property(c => c.UpdatedAt);
        builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);
    }
}