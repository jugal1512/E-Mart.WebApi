using AutoMapper;
using E_Mart.Domain.Categories;
using E_Mart.Domain.Products;
using E_Mart.WebApi.Models;
using E_Mart.WebApi.Models.Product;
using E_Mart.WebApi.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace E_Mart.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly IMapper _mapper;
    public ProductController(ProductService productService, CategoryService categoryService, IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _productService.GetAllAsync();
            var productList = _mapper.Map<List<ProductViewModal>>(products);
            return Ok(productList);
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin,Seller")]
    [HttpPost]
    [Route("AddProduct")]
    public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto)
    {
        try
        {
            string userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var productImageNameList = await SaveProductImages(productDto.ProductImage);
            var categoryExist = await _categoryService.GetCategoryByName(productDto.CategoryName);
            if (categoryExist == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Category is Not Exists!" });
            }
            else
            {
                productDto.CategoryId = categoryExist.Id;
            }
            var product = _mapper.Map<Product>(productDto);
            product.ProductImage = productImageNameList;
            product.CreatedBy = userName;
            await _productService.AddAsync(product);
            return Ok(new Response { Status = "Success", Message = "Product Add Successfully." });
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin,Seller")]
    [HttpPut]
    [Route("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([FromForm]ProductDto productDto)
    {
        try
        {
            string userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var productExist = await _productService.GetByIdAsync(productDto.Id);
            if (!string.IsNullOrEmpty(productDto.CategoryName))
            {
                var categoryExist = await _categoryService.GetCategoryByName(productDto.CategoryName);
                if (categoryExist == null) {
                    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Category is Not Exists!" });
                }
                productDto.CategoryId = productExist.CategoryId;
            }
            var newProductImageNameList = productExist.ProductImage;
            if (productDto.ProductImage != null && productDto.ProductImage.Count > 0)
            { 
                List<string> oldProductImageNames = productExist.ProductImage.Split(",").ToList();
                DeleteProductImage(oldProductImageNames);

                newProductImageNameList = await SaveProductImages(productDto.ProductImage);
            }
            var product = _mapper.Map<Product>(productDto);
            product.IsActive = true;
            product.ProductImage = newProductImageNameList;
            product.CreatedAt = productExist.CreatedAt;
            product.CreatedBy = productExist.CreatedBy;
            product.UpdatedBy = userName;
            await _productService.UpdateAsync(product);
            return Ok(new Response { Status = "Success", Message = "Product Update Successfully." });
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin,Seller")]
    [HttpDelete]
    [Route("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try {
            var productExist = await _productService.GetByIdAsync(id);
            List<string> productImageNameList = productExist.ProductImage.Split(',').ToList();
            DeleteProductImage(productImageNameList);
            await _productService.DeleteAsync(id);
            return Ok(new Response { Status = "Success", Message = "Product Add Successfully." });
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpGet]
    [Route("SearchProduct")]
    public async Task<IActionResult> SearchProduct(string productName)
    {
        try {
            Expression<Func<Product, bool>> predicate = p => p.ProductName.ToLower().Contains(productName.ToLower());
            var productList = await _productService.SearchProduct(predicate);
            return Ok(productList);
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }
    private async Task<string> SaveProductImages(List<IFormFile> productImages)
    {
        List<string> productImageList = new List<string>();
        foreach (var item in productImages)
        {
            var ProductImageName = Guid.NewGuid() + "_" + item.FileName;
            var directorypath = Path.Combine(Directory.GetCurrentDirectory(), "Images/ProductImages");
            if (!Directory.Exists(directorypath))
            {
                Directory.CreateDirectory(directorypath);
            }
            var productImagePath = Path.Combine(directorypath, ProductImageName);
            using (var stream = new FileStream(productImagePath, FileMode.Create))
            {
                await item.CopyToAsync(stream);
            }
            productImageList.Add(ProductImageName);
        }
        return string.Join(",", productImageList);
    }

    private void DeleteProductImage(List<string> productImageNameList)
    {
        foreach (var item in productImageNameList)
        {
            var oldProductImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images/ProductImages", item);
            if (System.IO.File.Exists(oldProductImagePath))
            {
                System.IO.File.Delete(oldProductImagePath);
            }
        }
    }
}