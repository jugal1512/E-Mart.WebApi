namespace E_Mart.WebApi.Models.Product;

public class ProductImagesViewModal
{
    public List<IFormFile> ImageUrl { get; set; }
    public bool IsPrimary { get; set; }
}