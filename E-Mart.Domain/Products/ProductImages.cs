using E_Mart.Domain.Base;

namespace E_Mart.Domain.Products;
public class ProductImages : BaseEntity
{
    public int ProductId { get; set; }
    public string ImageUrl { get; set; }
    public bool IsPrimary { get; set; }
    public Product? Product { get; set; }
}