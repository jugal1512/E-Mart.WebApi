using E_Mart.Domain.Carts;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Repositories;
public class CartItemRepository : GenericRepository<CartItem, EMartDbContext>, ICartItemRepository
{
    private readonly EMartDbContext _emartDbContext;
    public CartItemRepository(EMartDbContext eMartDbContext) : base(eMartDbContext)
    {
        _emartDbContext = eMartDbContext;
    }
}
