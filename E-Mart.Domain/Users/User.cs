using E_Mart.Domain.Carts;
using E_Mart.Domain.Users;
using E_Mart.Domain.Wishlists;
using System.ComponentModel.DataAnnotations;
namespace E_Mart.Domain.Customer;
public class User
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Role Role { get; set; }
    public virtual List<UserDetails> UserDetails { get; set; }
    public virtual ICollection<wishlist> Wishlists { get; set; }
    public virtual ICollection<Cart> Carts { get; set; }
}