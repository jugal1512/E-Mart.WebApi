using AutoMapper;
using E_Mart.Domain.Categories;
using E_Mart.Domain.Products;
using E_Mart.WebApi.Models;
using E_Mart.WebApi.Models.Product;
using E_Mart.WebApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    [Route("AddProduct")]
    public async Task<IActionResult> AddProduct([FromForm]ProductDto productDto)
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
            return Ok(new Response { Status = "Success", Message = "Product Add Successfully."});
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }


    [HttpPut]
    [Route("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct(ProductDto productDto)
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
                productDto.CategoryId = productExist.Id;
            }
            var newProductImageNameList = productExist.ProductImage;
            if (productDto.ProductImage != null && productDto.ProductImage.Count > 0)
            { 
                List<string> oldProductImageNames = productExist.ProductImage.Split(",").ToList();
                DeleteProductImage(oldProductImageNames);

                newProductImageNameList = await SaveProductImages(productDto.ProductImage);
            }
            var product = _mapper.Map<Product>(productDto);
            product.ProductImage = newProductImageNameList;
            product.UpdatedBy = userName;
            return Ok(product);
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

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