using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Products;
public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> SearchProductAsync(Expression<Func<Product, bool>> predicate);
    Task<int> GetProductPriceAsync(int id);
}