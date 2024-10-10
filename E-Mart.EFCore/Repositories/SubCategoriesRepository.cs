using E_Mart.Domain.Categories;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Repositories;
public class SubCategoriesRepository : GenericRepository<SubCategories, EMartDbContext>, ISubCategoriesRepository
{
    private readonly EMartDbContext _eMartDbContext;
    public SubCategoriesRepository(EMartDbContext eMartDbContext) : base(eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
    }
}
