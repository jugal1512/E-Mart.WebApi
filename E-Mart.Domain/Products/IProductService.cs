using E_Mart.Domain.Base;
using System.Linq.Expressions;

namespace E_Mart.Domain.Products;
public interface IProductService : IGenericService<Product>
{
    Task<List<Product>> SearchProductAsync(Expression<Func<Product, bool>> predicate);
    Task<int> GetProductPriceAsync(int id);
}