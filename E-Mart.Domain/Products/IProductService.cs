using E_Mart.Domain.Base;
using System.Linq.Expressions;

namespace E_Mart.Domain.Products;
public interface IProductService : IGenericService<Product>
{
    Task<List<Product>> SearchProduct(Expression<Func<Product, bool>> predicate);
}
