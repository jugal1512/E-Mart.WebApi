using E_Mart.Domain.Base;
using E_Mart.Domain.Categories;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Mart.EFCore.Repositories;
public class CategoryRepository : GenericRepository<Category,EMartDbContext> , ICategoryRepository
{
    private readonly EMartDbContext _eMartDbContext;

    public CategoryRepository(EMartDbContext eMartDbContext) : base(eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
    }

    public async Task<Category> GetCategoryByName(string categoryName)
    {
        return await _eMartDbContext.Categories.Where(x => x.CategoryName == categoryName).FirstOrDefaultAsync();
    }
}