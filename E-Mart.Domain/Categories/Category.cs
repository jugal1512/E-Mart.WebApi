using E_Mart.Domain.Base;

namespace E_Mart.Domain.Categories;
public class Category : BaseEntity
{
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public string CategoryImage { get; set; }
    public virtual ICollection<SubCategories> SubCategories { get; set; }
}