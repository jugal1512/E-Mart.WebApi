using E_Mart.Domain.Base;
using E_Mart.Domain.Customer;

namespace E_Mart.Domain.Carts;
public class Cart : BaseEntity
{
    public int UserId { get; set; }
    public decimal Total { get; set; }
    public virtual User User { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
}