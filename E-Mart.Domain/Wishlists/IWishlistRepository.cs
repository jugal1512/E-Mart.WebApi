using E_Mart.Domain.Base;

namespace E_Mart.Domain.Wishlists;
public interface IWishlistRepository :IGenericRepository<wishlist>
{
    Task<List<wishlist>> getwishListsByUserAsync(int userId);
    Task ClearWishlistAsync(int userId);
}