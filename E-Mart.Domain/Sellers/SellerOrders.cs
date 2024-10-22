using E_Mart.Domain.Base;

namespace E_Mart.Domain.Sellers;
public class SellerOrders : BaseEntity
{
    public int OrderId { get; set; }
    public int SellerId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderDetails.OrderDetails OrderDetails { get; set; }
    public Seller Seller { get; set; }
}