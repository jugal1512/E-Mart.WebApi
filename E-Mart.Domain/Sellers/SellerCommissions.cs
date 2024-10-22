using E_Mart.Domain.Base;

namespace E_Mart.Domain.Sellers;
public class SellerCommissions : BaseEntity
{
    public int SellerId { get; set; }
    public decimal CommissionRate { get; set; }
    public DateTime EffectiveDate { get; set; }
    public Seller Seller { get; set; }
}