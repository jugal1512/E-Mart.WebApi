using E_Mart.Domain.Carts;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;
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

    public async Task<CartItem> GetCartItemByIdAsync(int itemId)
    {
        return await _eMartDbContext.CartItems.AsNoTracking().Where(x => x.Id == itemId).FirstOrDefaultAsync();
    }

    public async Task<List<CartItem>> GetCartItemsBycartIdAsync(int cartId)
    {
        return await _eMartDbContext.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
    }

    public async Task RemoveCartItemAsync(int itemId)
    {
        var cartItem = await GetCartItemByIdAsync(itemId);
        _eMartDbContext.Remove(cartItem);
        await _eMartDbContext.SaveChangesAsync();
    }

    public async Task RemoveCartItemsAsync(int cartId)
    {
        var cartItems = await GetCartItemsBycartIdAsync(cartId);
        _eMartDbContext.RemoveRange(cartItems);
        await _eMartDbContext.SaveChangesAsync();
    }

    public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
    {
        _eMartDbContext.Update(cartItem);
        await _eMartDbContext.SaveChangesAsync();
        return cartItem;
    }
}
