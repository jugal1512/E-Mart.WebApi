using E_Mart.Domain.Base;
using E_Mart.Domain.Customer;

namespace E_Mart.Domain.Sellers;
public class SellerReviews : BaseEntity
{
    public int SellerId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public Seller Seller { get; set; }
    public User User { get; set; }
}