using E_Mart.Domain.Carts;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Repositories;
public class CartRepository : GenericRepository<Cart, EMartDbContext>, ICartRepository
{
    private readonly EMartDbContext _eMartDbContext;
    public CartRepository(EMartDbContext eMartDbContext) : base(eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
    }
}
