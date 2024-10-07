using E_Mart.Domain.Base;
using E_Mart.Domain.Customer;
using E_Mart.Domain.Products;

namespace E_Mart.Domain.Wishlists;
public class wishlist : BaseEntity
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public DateTime? DeletedAt { get; set; }
    public virtual User User { get; set; }
    public virtual Product Product { get; set; }
}
