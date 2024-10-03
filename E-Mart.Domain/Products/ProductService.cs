using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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
}
