using E_Mart.Domain.Products;

namespace E_Mart.WebApi.Models;

public class CategoryDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public IFormFile? CategoryImage { get; set; }
    public bool? IsActive { get; set; }
}