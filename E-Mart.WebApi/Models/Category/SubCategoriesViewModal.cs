namespace E_Mart.WebApi.Models.Category;

public class SubCategoriesViewModal
{
    public int Id { get; set; }
    public int ParentCategoryId { get; set; }
    public string Name { get; set; }
    public string CategoryImage { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }
    public virtual CategoryViewModal? Category { get; set; }
}