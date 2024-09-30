using E_Mart.Domain.Products;
using System.ComponentModel.DataAnnotations;

namespace E_Mart.Domain.Categories;
public class Category
{
    [Key]
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public string CategoryImage { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
