using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Wishlists;
public interface IWishlistService : IGenericService<wishlist>
{
    Task<List<wishlist>> getwishListsByUserAsync(int userId);
    Task ClearWishlistAsync(int userId);
}
