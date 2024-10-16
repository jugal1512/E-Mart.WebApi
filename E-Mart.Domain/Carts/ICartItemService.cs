using E_Mart.Domain.Base;

namespace E_Mart.Domain.Carts;
public interface ICartItemService
{
    Task<CartItem> AddCartItemAsync(CartItem cartItem);
}