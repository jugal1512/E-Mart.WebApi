using E_Mart.Domain.Base;
using E_Mart.Domain.Products;

namespace E_Mart.Domain.Carts;
public class CartItem : BaseEntity
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public virtual Cart Cart { get; set; }
    public virtual Product Product { get; set; }
}