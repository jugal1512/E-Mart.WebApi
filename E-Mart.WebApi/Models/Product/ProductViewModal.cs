namespace E_Mart.WebApi.Models.Product;

public class ProductViewModal
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int OriginalPrice { get; set; }
    public int ActualPrice { get; set; }
    public int Stock { get; set; }
    public int? CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string ProductImage { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

}
