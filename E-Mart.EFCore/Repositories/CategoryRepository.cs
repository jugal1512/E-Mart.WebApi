using E_Mart.Domain.Base;
using E_Mart.Domain.Categories;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<List<Category>> SearchCategory(Expression<Func<Category, bool>> predicate)
    {
        return await _eMartDbContext.Categories.AsQueryable().Where(predicate).ToListAsync();
    }
}