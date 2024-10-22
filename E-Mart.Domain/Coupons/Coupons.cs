using E_Mart.Domain.Base;

namespace E_Mart.Domain.Coupons;
public class Coupons : BaseEntity
{
    public string Code { get; set; }
    public string DiscountType { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}