using AutoMapper;
using E_Mart.Domain.Categories;
using E_Mart.Domain.Products;
using E_Mart.WebApi.Models.Product;
using E_Mart.WebApi.Models.Response;
using E_Mart.WebApi.Utilities.FirebaseImageUpload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace E_Mart.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    //private readonly ProductService _productService;
    //private readonly SubCategoriesService _subCategoriesService;
    //private readonly string _fileUploadFolder;
    //private readonly IFirebaseImageUploadService _firebaseImageUploadService;
    //private readonly IMapper _mapper;
    //public ProductController(ProductService productService, SubCategoriesService subCategoriesService, IMapper mapper,IConfiguration configuration, IFirebaseImageUploadService firebaseImageUploadService)
    //{
    //    _productService = productService;
    //    _subCategoriesService = subCategoriesService;
    //    _fileUploadFolder = configuration["FileUploadSettings:ProductPage"];
    //    _firebaseImageUploadService = firebaseImageUploadService;
    //    _mapper = mapper;
    //} 

    //[HttpGet]
    //[Route("GetAllProducts")]
    //public async Task<IActionResult> GetAllProducts()
    //{
    //    try
    //    {
    //        var products = await _productService.GetAllAsync();
    //        var productList = _mapper.Map<List<ProductViewModal>>(products);
    //        return Ok(productList);
    //    }
    //    catch (Exception ex) {
    //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
    //    }
    //}

    //[Authorize(Roles = "Admin,Seller")]
    //[HttpPost]
    //[Route("createProductAsync")]
    //public async Task<IActionResult> CreateProductAsync([FromForm] ProductDto productDto)
    //{
    //    try
    //    {
    //        string userName = User.FindFirst(ClaimTypes.Name)?.Value;
    //        var subCategoryExist = await _subCategoriesService.GetSubCategoryByNameAsync(productDto.SubCategoryName);
    //        if (subCategoryExist == null)
    //        {
    //            return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Sub Category is Not Exists!" });
    //        }
    //        var productImageNameList = await SaveProductImagesAsync(productDto.ProductImage);
    //        var product = _mapper.Map<Product>(productDto);
    //        product.ProductImage = productImageNameList;
    //        product.CreatedBy = userName;
    //        product.CategoryId = subCategoryExist.Id;
    //        await _productService.AddAsync(product);
    //        return Ok(new Response { Status = "Success", Message = "Product Add Successfully." });
    //    }
    //    catch (Exception ex) {
    //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
    //    }
    //}

    //[Authorize(Roles = "Admin,Seller")]
    //[HttpPut]
    //[Route("updateProductAsync")]
    //public async Task<IActionResult> UpdateProductAsync([FromForm] ProductUpdateViewModal productDto)
    //{
    //    try
    //    {
    //        string userName = User.FindFirst(ClaimTypes.Name)?.Value;
    //        var productExist = await _productService.GetByIdAsync(productDto.Id);
    //        var subCategoriesId = productExist.CategoryId;
    //        if (!string.IsNullOrEmpty(productDto.SubCategoryName))
    //        {
    //            var subCategoryExist = await _subCategoriesService.GetSubCategoryByNameAsync(productDto.SubCategoryName);
    //            if (subCategoryExist == null)
    //            {
    //                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Category is Not Exists!" });
    //            }
    //            else { 
    //                subCategoriesId = subCategoryExist.Id;
    //            }
    //        }
    //        List<string> existingImages = productExist.ProductImage.Split(",").ToList();
    //        if (!string.IsNullOrEmpty(productDto.deletedImageNames))
    //        {
    //            List<string> oldDeleteProductImageNames = productDto.deletedImageNames.Split(",").ToList();
    //            await DeleteProductImageAsync(oldDeleteProductImageNames);
    //            foreach (var img in oldDeleteProductImageNames)
    //            {
    //                existingImages.Remove(img);
    //            }
    //        }
    //        if (productDto.ProductImage != null)
    //        {
    //            List<string> newProductImages = (await SaveProductImagesAsync(productDto.ProductImage)).Split(",").ToList();
    //            foreach (var img in newProductImages)
    //            {
    //                existingImages.Add(img);
    //            }
    //        }
    //        var product = _mapper.Map<Product>(productDto);
    //        product.IsActive = true;
    //        product.CategoryId = subCategoriesId;
    //        product.CreatedAt = productExist.CreatedAt;
    //        product.CreatedBy = productExist.CreatedBy;
    //        product.UpdatedBy = userName;
    //        product.ProductImage = string.Join(",", existingImages);
    //        await _productService.UpdateAsync(product);
    //        return Ok(new Response { Status = "Success", Message = "Product Updated Successfully." });
    //    }
    //    catch (Exception ex) {
    //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
    //    }
    //}

    //[Authorize(Roles = "Admin,Seller")]
    //[HttpDelete]    
    //[Route("deleteProductAsync/{id}")]
    //public async Task<IActionResult> DeleteProductAsync(int id)
    //{
    //    try {
    //        var productExist = await _productService.GetByIdAsync(id);
    //        List<string> productImageNameList = productExist.ProductImage.Split(',').ToList();
    //        await DeleteProductImageAsync(productImageNameList);
    //        await _productService.SoftDeleteAsync(id);
    //        return Ok(new Response { Status = "Success", Message = "Product Deleted Successfully." });
    //    }
    //    catch (Exception ex) {
    //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
    //    }
    //}

    //[HttpGet]
    //[Route("searchProductAsync/{productName}")]
    //public async Task<IActionResult> SearchProductAsync(string productName)
    //{
    //    try {
    //        Expression<Func<Product, bool>> predicate = p => p.ProductName.ToLower().Contains(productName.ToLower());
    //        var productList = await _productService.SearchProductAsync(predicate);
    //        return Ok(productList);
    //    }
    //    catch (Exception ex) {
    //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
    //    }
    //}

    //private async Task<string> SaveProductImagesAsync(List<IFormFile> productImages)
    //{
    //    List<string> productImageList = new List<string>();
    //    foreach (var item in productImages)
    //    {
    //        var ProductImageName = Guid.NewGuid() + "_" + item.FileName;
    //        var filePath = Path.Combine(Path.GetTempPath(), ProductImageName);
    //        var fileUploadFolder = _fileUploadFolder;
    //        using (var stream = new FileStream(filePath, FileMode.Create))
    //        {
    //            await item.CopyToAsync(stream);
    //        }
    //        productImageList.Add(ProductImageName);
    //        var firebaseImageUpload = new FirebaseImageUploadModal
    //        {
    //            fileUploadFolder = fileUploadFolder,
    //            fileName = ProductImageName,
    //            filePath = filePath,
    //        };
    //        var downloadUrl = await _firebaseImageUploadService.FirebaseUploadImageAsync(firebaseImageUpload);
    //    }
    //    return string.Join(",", productImageList);
    //}

    //private async Task DeleteProductImageAsync(List<string> productImageNameList)
    //{
    //    foreach (var item in productImageNameList)
    //    {
    //        var fileUploadFolder = _fileUploadFolder;
    //        var firebaseGetImage = new FirebaseImageUploadModal
    //        {
    //            fileUploadFolder = fileUploadFolder,
    //            fileName = item,
    //        };
    //        await _firebaseImageUploadService.FirebaseDeleteUploadImageAsync(firebaseGetImage);
    //    }
    //}
}