using E_Mart.Domain.Base;
using E_Mart.Domain.Customer;

namespace E_Mart.Domain.Sellers;
public class Seller : BaseEntity
{
    public int UserId { get; set; }
    public string StoreName { get; set; }
    public string StoreDescription { get; set; }
    public string? StoreLogo { get; set; }
    public User User { get; set; }
}