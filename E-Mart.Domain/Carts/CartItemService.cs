using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Carts;
public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;
    public CartItemService(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }
    public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
    {
        return await _cartItemRepository.AddCartItemAsync(cartItem);
    }

    public async Task<CartItem> GetCartItemByIdAsync(int itemId)
    {
        return await _cartItemRepository.GetCartItemByIdAsync(itemId);
    }

    public async Task<List<CartItem>> GetCartItemsBycartIdAsync(int cartId)
    {
        return await _cartItemRepository.GetCartItemsBycartIdAsync(cartId);
    }

    public async Task RemoveCartItemAsync(int itemId)
    {
        await _cartItemRepository.RemoveCartItemAsync(itemId);
    }

    public async Task RemoveCartItemsAsync(int cartId)
    {
        await _cartItemRepository.RemoveCartItemsAsync(cartId);
    }

    public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
    {
        return await _cartItemRepository.UpdateCartItemAsync(cartItem);
    }
}