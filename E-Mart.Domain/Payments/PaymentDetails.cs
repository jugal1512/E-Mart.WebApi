using E_Mart.Domain.Base;

namespace E_Mart.Domain.Payments;
public class PaymentDetails : BaseEntity
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Provider { get; set; }
    public bool Status { get; set; }
    public virtual OrderDetails.OrderDetails OrderDetails { get; set; }
}