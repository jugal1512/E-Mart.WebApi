using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Carts;
public interface ICartItemRepository
{
    Task<List<CartItem>> GetCartItemsBycartIdAsync(int cartId);
    Task<CartItem> AddCartItemAsync(CartItem cartItem);
    Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
    Task<CartItem> GetCartItemByIdAsync(int itemId);
    Task RemoveCartItemAsync(int itemId);
    Task RemoveCartItemsAsync(int cartId);
}
