using E_Mart.Domain.Base;
using System.Linq.Expressions;

namespace E_Mart.Domain.Categories;
public interface ICategoryService : IGenericService<Category>
{
    Task<Category> GetCategoryByNameAsync(string categoryName);
    Task<List<Category>> SearchCategoryAsync(Expression<Func<Category, bool>> predicate);
}