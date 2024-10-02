using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Categories;
public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category> GetCategoryByName(string categoryName);
}
