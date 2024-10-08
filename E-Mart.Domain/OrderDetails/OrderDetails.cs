using E_Mart.Domain.Base;
using E_Mart.Domain.Customer;
using E_Mart.Domain.Payments;

namespace E_Mart.Domain.OrderDetails;
public class OrderDetails : BaseEntity
{
    public int UserId { get; set; }
    public double TotalAmount { get; set; }
    public virtual User User { get; set; }
    public virtual PaymentDetails PaymentDetails { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}