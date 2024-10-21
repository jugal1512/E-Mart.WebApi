using E_Mart.Domain.Base;

namespace E_Mart.Domain.Wishlists;
public class WishlistService : GenericService<wishlist>, IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;
    public WishlistService(IWishlistRepository wishlistRepository) : base(wishlistRepository)
    {
        _wishlistRepository = wishlistRepository;
    }

    public async Task ClearWishlistAsync(int userId)
    {
        await _wishlistRepository.ClearWishlistAsync(userId);
    }

    public async Task<List<wishlist>> getwishListsByUserAsync(int userId)
    {
        return await _wishlistRepository.getwishListsByUserAsync(userId);
    }
}