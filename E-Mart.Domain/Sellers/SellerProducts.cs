using E_Mart.Domain.Base;
using E_Mart.Domain.Products;

namespace E_Mart.Domain.Sellers;
public class SellerProducts : BaseEntity
{
    public int SellerId { get; set; }
    public int ProductId { get; set; }
    public Seller Seller { get; set; }
    public Product Product { get; set; }
}