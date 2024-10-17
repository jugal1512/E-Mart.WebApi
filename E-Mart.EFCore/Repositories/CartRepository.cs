using E_Mart.Domain.Carts;
using E_Mart.Domain.Categories;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Repositories;
public class CartRepository : GenericRepository<Cart, EMartDbContext>, ICartRepository
{
    private readonly EMartDbContext _eMartDbContext;
    public CartRepository(EMartDbContext eMartDbContext) : base(eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
    }

    public override async Task<IEnumerable<Cart>> GetAllAsync()
    {
        return await _eMartDbContext.Carts.AsNoTracking().Include(c => c.CartItems).ThenInclude(ci => ci.Product).Where(x => x.IsActive).ToListAsync();
    }

    public async Task<Cart> getCartDetilsByUserIdAsync(int id)
    {
        return await _eMartDbContext.Carts.AsNoTracking().Where(c => c.UserId == id).Include(ci => ci.CartItems).ThenInclude(p => p.Product).FirstOrDefaultAsync();
    }
}