using E_Mart.Domain.Products;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Repositories;
public class ProductRepository : GenericRepository<Product, EMartDbContext>, IProductRepository
{
    private readonly EMartDbContext _eMartDbContext;
    public ProductRepository(EMartDbContext eMartDbContext) : base(eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
    }

    public async Task<int> GetProductPriceAsync(int id)
    {
        return await _eMartDbContext.Products.AsNoTracking().Where(p => p.Id == id ).Select(p => p.OriginalPrice).FirstOrDefaultAsync();
    }

    public async Task<List<Product>> SearchProductAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _eMartDbContext.Products.Where(predicate).ToListAsync();
    }
}
