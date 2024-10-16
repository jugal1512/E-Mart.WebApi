using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Carts;
public interface ICartItemRepository
{
    Task<CartItem> AddCartItemAsync(CartItem cartItem);
}
