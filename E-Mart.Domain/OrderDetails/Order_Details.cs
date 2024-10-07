using E_Mart.Domain.Base;
using E_Mart.Domain.Customer;
using E_Mart.Domain.Payments;

namespace E_Mart.Domain.OrderDetails;
public class Order_Details : BaseEntity
{
    public int UserId { get; set; }
    public double TotalAmount { get; set; }
    public virtual User User { get; set; }
    public virtual Payment_Details Payment_Details { get; set; }
    public virtual ICollection<Order_Item> Order_Items { get; set; }
}