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
}