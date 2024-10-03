﻿using E_Mart.Domain.Products;
using E_Mart.EFCore.Base;
using E_Mart.EFCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Repositories;
public class ProductRepository : GenericRepository<Product, EMartDbContext>, IProductRepository
{
    private readonly EMartDbContext _eMartDbContext;
    public ProductRepository(EMartDbContext eMartDbContext) : base(eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
    }
}
