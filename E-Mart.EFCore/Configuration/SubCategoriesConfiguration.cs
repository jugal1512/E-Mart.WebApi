using E_Mart.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Mart.EFCore.Configuration;
public class SubCategoriesConfiguration : IEntityTypeConfiguration<SubCategories>
{
    public void Configure(EntityTypeBuilder<SubCategories> builder)
    {
        builder.ToTable("SubCategories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.ParentCategoryId).IsRequired();
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Description).IsRequired().HasMaxLength(250);
        builder.Property(c => c.CategoryImage).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt);
        builder.Property(c => c.IsDeleted).HasColumnType("bit").HasDefaultValue(false);

        builder
            .HasOne(sc => sc.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(sc => sc.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}