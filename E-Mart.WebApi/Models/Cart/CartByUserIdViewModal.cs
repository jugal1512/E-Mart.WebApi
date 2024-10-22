using E_Mart.Domain.Carts;

namespace E_Mart.WebApi.Models.Cart;

public class CartByUserIdViewModal
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
}
