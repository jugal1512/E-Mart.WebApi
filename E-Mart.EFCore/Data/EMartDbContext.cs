using E_Mart.Domain.Carts;
using E_Mart.Domain.Categories;
using E_Mart.Domain.Customer;
using E_Mart.Domain.OrderDetails;
using E_Mart.Domain.Payments;
using E_Mart.Domain.Products;
using E_Mart.Domain.Users;
using E_Mart.Domain.Wishlists;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_Mart.EFCore.Data;
public class EMartDbContext:DbContext
{
    public EMartDbContext(DbContextOptions<EMartDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserDetails> UserDetails { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategories> SubCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PaymentDetails> PaymentDetails { get; set; }
    public DbSet<wishlist> Wishlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetAssembly(typeof(EMartDbContext)));
    }
}