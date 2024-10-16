using E_Mart.Domain.Carts;
using E_Mart.EFCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Repositories;
public class CartItemRepository : ICartItemRepository
{
    private readonly EMartDbContext _eMartDbContext;
    public CartItemRepository(EMartDbContext eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
    }
    public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
    {
        await _eMartDbContext.CartItems.AddAsync(cartItem);
        await _eMartDbContext.SaveChangesAsync();
        return cartItem;
    }
}
