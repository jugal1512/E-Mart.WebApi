using E_Mart.Domain.Base;

namespace E_Mart.Domain.Sellers;
public class SellerPayments : BaseEntity
{
    public int SellerId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; }
    public Seller Seller { get; set; }
}