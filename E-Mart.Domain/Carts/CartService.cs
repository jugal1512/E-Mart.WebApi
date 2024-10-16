using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Carts;
public class CartService : GenericService<Cart>, ICartService
{
    private readonly ICartRepository _cartRepository;
    public CartService(ICartRepository cartRepository) : base(cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Cart> getCartDetilsByUserIdAsync(int id)
    {
        return await _cartRepository.getCartDetilsByUserIdAsync(id);
    }
}
