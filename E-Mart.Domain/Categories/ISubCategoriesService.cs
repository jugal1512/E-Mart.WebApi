using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Categories;
public interface ISubCategoriesService : IGenericService<SubCategories>
{
    Task<SubCategories> GetSubCategoryByNameAsync(string subCategoryName);
    Task<List<SubCategories>> SearchSubCategoryAsync(Expression<Func<SubCategories, bool>> predicate);
}