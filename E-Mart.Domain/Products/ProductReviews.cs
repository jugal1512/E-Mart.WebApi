using E_Mart.Domain.Base;
using E_Mart.Domain.Customer;

namespace E_Mart.Domain.Products;
public class ProductReviews : BaseEntity
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public Product Product { get; set; }
    public User User { get; set; }
}