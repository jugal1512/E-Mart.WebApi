using E_Mart.Domain.Wishlists;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Mart.EFCore.Repositories;
public class WishlistRepository : GenericRepository<wishlist, EMartDbContext>, IWishlistRepository
{
    private readonly EMartDbContext _emartDbContext;
    public WishlistRepository(EMartDbContext eMartDbContext) : base(eMartDbContext)
    {
        _emartDbContext = eMartDbContext;
    }

    public async Task ClearWishlistAsync(int userId)
    {
        var wishlists = await getwishListsByUserAsync(userId);
        _emartDbContext.RemoveRange(wishlists);
        await _emartDbContext.SaveChangesAsync();
    }

    public async Task<List<wishlist>> getwishListsByUserAsync(int userId)
    {
        return await _emartDbContext.Wishlists.AsNoTracking().Where(w => w.UserId == userId && w.IsActive).Include(p => p.Product).ToListAsync();
    }
}