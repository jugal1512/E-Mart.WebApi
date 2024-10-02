using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Categories;
public class CategoryService : GenericService<Category> , ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> GetCategoryByName(string categoryName)
    {
        return await _categoryRepository.GetCategoryByName(categoryName);
    }
}