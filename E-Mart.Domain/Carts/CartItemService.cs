using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Carts;
public class CartItemService : GenericService<CartItem>, ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;
    public CartItemService(ICartItemRepository cartItemRepository) : base(cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }
}
