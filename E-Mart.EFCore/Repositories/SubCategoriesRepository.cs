using E_Mart.Domain.Categories;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Mart.EFCore.Repositories;
public class SubCategoriesRepository : GenericRepository<SubCategories, EMartDbContext>, ISubCategoriesRepository
{
    private readonly EMartDbContext _eMartDbContext;
    public SubCategoriesRepository(EMartDbContext eMartDbContext) : base(eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
    }

    public override async Task<IEnumerable<SubCategories>> GetAllAsync()
    { 
        return await _eMartDbContext.SubCategories.AsNoTracking().Include(c =>c.Category).Where(x => x.IsActive).ToListAsync();
    }

    public async Task<SubCategories> GetSubCategoryByNameAsync(string subCategoryName)
    {
        return await _eMartDbContext.SubCategories.Where(x => x.Name == subCategoryName && x.IsActive == true).FirstOrDefaultAsync();
    }

    public async Task<List<SubCategories>> SearchSubCategoryAsync(Expression<Func<SubCategories, bool>> predicate)
    {
        return await _eMartDbContext.SubCategories.AsQueryable().Where(predicate).ToListAsync();
    }
}