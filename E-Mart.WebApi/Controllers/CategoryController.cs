using AutoMapper;
using E_Mart.Domain.Categories;
using E_Mart.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace E_Mart.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;
    private readonly IMapper _mapper;
    public CategoryController(CategoryService categoryService,IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetAllCategory")]
    public async Task<IActionResult> GetAllCategory()
    {
        try
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Category not Found!" });
            }
            var categoryList = _mapper.Map<List<CategoryViewModal>>(categories);
            return Ok(categoryList);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("AddCategory")]
    public async Task<IActionResult> AddCategory([FromForm] CategoryDto categoryDto)
    {
        try
        {
            var categoryExists = await _categoryService.GetCategoryByName(categoryDto.CategoryName);
            if (categoryExists != null) {
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Error", Message = "Category Already Have Exist!" });
            }
            var categoryImageName = await SaveCategoryImage(categoryDto.CategoryImage);
            var category = _mapper.Map<Category>(categoryDto);
            category.CategoryImage = categoryImageName;
            var addCategory = await _categoryService.AddAsync(category);
            return Ok( new Response{Status = "Success",Message = "Category Added Successfully." });
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory([FromForm] CategoryDto categoryDto)
    {
        try
        {
            var categoryExists = await _categoryService.GetByIdAsync(categoryDto.Id);
            var categoryImageName = categoryExists.CategoryImage;
            if (categoryDto.CategoryImage != null)
            {
                DeleteOldCategoryImage(categoryExists.CategoryImage);
                categoryImageName = await SaveCategoryImage(categoryDto.CategoryImage);
            }
            var category = _mapper.Map<Category>(categoryDto);
            category.CategoryImage = categoryImageName;
            category.UpdatedAt = DateTime.Now;
            var updateCategory = await _categoryService.UpdateAsync(category);
            return Ok(new Response { Status = "Success", Message = "Category Updated Succesfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var category = await _categoryService.GetByIdAsync(id);
            DeleteOldCategoryImage(category.CategoryImage);
            await _categoryService.DeleteAsync(category.Id);
            return Ok(new Response {Status = "Success",Message = "Category Deleted Successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpGet]
    [Route("SearchCategory")]
    public async Task<IActionResult> SearchCategory(string categoryName)
    {
        try
        {
            Expression<Func<Category, bool>> predicate = c => c.CategoryName.ToLower().Contains(categoryName.ToLower());
            var category = await _categoryService.SearchCategory(predicate);
            if (category == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Category Not Found!" });
            }
            return Ok(category);
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    private async Task<string> SaveCategoryImage(IFormFile categoryImage)
    {
        var categoryImageName = Guid.NewGuid() + "_" + categoryImage.FileName;
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Images/CategoryImages");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        var categoryImagePath = Path.Combine(directoryPath, categoryImageName);
        using (var stream = new FileStream(categoryImagePath, FileMode.Create))
        {
            await categoryImage.CopyToAsync(stream);
        }
        return categoryImageName;
    }

    private void DeleteOldCategoryImage(string categoryImage)
    {
        var oldImage = Path.Combine(Directory.GetCurrentDirectory(), "Images/CategoryImages", categoryImage);
        if (System.IO.File.Exists(oldImage))
        {
            System.IO.File.Delete(oldImage);
        }
    }
}