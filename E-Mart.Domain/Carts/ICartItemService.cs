using E_Mart.Domain.Base;

namespace E_Mart.Domain.Carts;
public interface ICartItemService
{
    Task<List<CartItem>> GetCartItemsBycartIdAsync(int cartId);
    Task<CartItem> AddCartItemAsync(CartItem cartItem);
    Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
    Task<CartItem> GetCartItemByIdAsync(int itemId);
    Task RemoveCartItemAsync(int itemId);
    Task RemoveCartItemsAsync(int cartId);
}