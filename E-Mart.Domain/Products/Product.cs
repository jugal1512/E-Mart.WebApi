using System.ComponentModel.DataAnnotations;
using E_Mart.Domain.Categories;

namespace E_Mart.Domain.Products;
public class Product
{
    [Key]
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public string ProductImage { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public virtual Category Category { get; set; }
}
