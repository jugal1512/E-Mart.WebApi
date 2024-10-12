using E_Mart.Domain.Categories;

namespace E_Mart.WebApi.Models.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int OriginalPrice { get; set; }
    public int ActualPrice { get; set; }
    public int Stock { get; set; }
    public int? CategoryId { get; set; }
    public string SubCategoryName { get; set; }
    public List<IFormFile> ProductImage { get; set; }

}