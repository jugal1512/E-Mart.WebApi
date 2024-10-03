using E_Mart.Domain.Categories;
using E_Mart.Domain.Customer;
using E_Mart.Domain.Products;
using E_Mart.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_Mart.EFCore.Data;
public class EMartDbContext:DbContext
{
    public EMartDbContext(DbContextOptions<EMartDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetAssembly(typeof(EMartDbContext)));
    }
}