using E_Mart.Domain.Base;
using E_Mart.Domain.Products;

namespace E_Mart.Domain.Categories;
public class Sub_Categories : BaseEntity
{
    public int ParentCategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
