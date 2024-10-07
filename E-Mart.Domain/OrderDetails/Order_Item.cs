using E_Mart.Domain.Base;
using E_Mart.Domain.Products;

namespace E_Mart.Domain.OrderDetails;
public class Order_Item : BaseEntity
{
    public int orderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public virtual Order_Details Order_Details { get; set; }
    public virtual Product Product { get; set; }
}