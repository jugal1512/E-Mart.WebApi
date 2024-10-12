using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Categories;
public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category> GetCategoryByNameAsync(string categoryName);
    Task<List<Category>> SearchCategoryAsync(Expression<Func<Category, bool>> predicate);
}
