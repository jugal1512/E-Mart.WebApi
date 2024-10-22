using E_Mart.Domain.Base;
using E_Mart.Domain.Categories;
using E_Mart.Domain.Sellers;

namespace E_Mart.Domain.Products;
public class Product : BaseEntity
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int OriginalPrice { get; set; }
    public int ActualPrice { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public virtual SubCategories SubCategories { get; set; }
    public virtual List<Seller> Sellers { get; set; }
    public virtual List<ProductImages> ProductImages { get; set; }
}