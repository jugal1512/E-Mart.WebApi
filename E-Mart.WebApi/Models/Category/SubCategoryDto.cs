namespace E_Mart.WebApi.Models.Category;

public class SubCategoryDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile CategoryImage { get; set; }
}