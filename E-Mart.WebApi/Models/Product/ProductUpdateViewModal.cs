namespace E_Mart.WebApi.Models.Product;

public class ProductUpdateViewModal
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int OriginalPrice { get; set; }
    public int ActualPrice { get; set; }
    public int Stock { get; set; }
    public string SubCategoryName { get; set; }
    public string? deletedImageNames { get; set; }
    public List<IFormFile>? ProductImage { get; set; }
}
