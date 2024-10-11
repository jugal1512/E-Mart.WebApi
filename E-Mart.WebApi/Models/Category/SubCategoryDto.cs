namespace E_Mart.WebApi.Models.Category;

public class SubCategoryDto
{
    public string CategoryName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile CategoryImage { get; set; }
}