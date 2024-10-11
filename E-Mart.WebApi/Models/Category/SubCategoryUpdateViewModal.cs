namespace E_Mart.WebApi.Models.Category;

public class SubCategoryUpdateViewModal
{
    public int Id { get; set; }
    public string? CategoryName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool isActive { get; set; }
    public bool isDeleted { get; set; }
    public IFormFile? CategoryImage { get; set; }
}
