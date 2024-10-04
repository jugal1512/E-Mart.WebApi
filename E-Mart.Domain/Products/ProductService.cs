using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Products;
public class ProductService : GenericService<Product>, IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository) : base(productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> SearchProduct(Expression<Func<Product, bool>> predicate)
    {
        return await _productRepository.SearchProduct(predicate);
    }
}
