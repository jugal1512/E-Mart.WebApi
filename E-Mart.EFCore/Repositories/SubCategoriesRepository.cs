using E_Mart.Domain.Categories;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;

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
}