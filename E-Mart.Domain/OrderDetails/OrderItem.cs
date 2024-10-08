using E_Mart.Domain.Base;
using E_Mart.Domain.Products;

namespace E_Mart.Domain.OrderDetails;
public class OrderItem : BaseEntity
{
    public int orderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public virtual OrderDetails OrderDetails { get; set; }
    public virtual Product Product { get; set; }
}