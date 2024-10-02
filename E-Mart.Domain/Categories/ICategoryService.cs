using E_Mart.Domain.Base;

namespace E_Mart.Domain.Categories;
public interface ICategoryService : IGenericService<Category>
{
    Task<Category> GetCategoryByName(string categoryName);
}