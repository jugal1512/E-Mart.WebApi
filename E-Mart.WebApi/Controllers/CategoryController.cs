using AutoMapper;
using E_Mart.Domain.Categories;
using E_Mart.WebApi.Models.Category;
using E_Mart.WebApi.Models.Response;
using E_Mart.WebApi.Utilities.FirebaseImageUpload;
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
    public CategoryController(CategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("getAllCategoryAsync")]
    public async Task<IActionResult> GetAllCategoryAsync()
    {
        try
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Category not Found!" });
            }
            var categoryList = _mapper.Map<List<CategoryViewModal>>(categories);
            return Ok(new DataResponseList<CategoryViewModal> { Data = categoryList, Status = "Success", Message = "Categories Get Successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [Authorize(Roles ="Admin")]
    [HttpPost]
    [Route("createCategoryAsync")]
    public async Task<IActionResult> CreateCategoryAsync([FromForm] CategoryDto categoryDto)
    {
        try
        {
            var categoryExists = await _categoryService.GetCategoryByNameAsync(categoryDto.CategoryName);
            if (categoryExists != null) {
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Error", Message = "Category Already Have Exist!" });
            }
            var category = _mapper.Map<Category>(categoryDto);
            var addCategory = await _categoryService.AddAsync(category);
            return Ok( new Response{Status = "Success",Message = "Category Added Successfully." });
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [Authorize(Roles ="Admin")]
    [HttpPut]
    [Route("updateCategoryAsync")]
    public async Task<IActionResult> UpdateCategoryAsync([FromForm] CategoryDto categoryDto)
    {
        try
        {
            var categoryExists = await _categoryService.GetByIdAsync(categoryDto.Id);
            var category = _mapper.Map<Category>(categoryDto);
            category.UpdatedAt = DateTime.Now;
            var updateCategory = await _categoryService.UpdateAsync(category);
            return Ok(new DataResponse<Category> { Data = updateCategory,Status = "Success", Message = "Category Updated Succesfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("deleteCategoryAsync/{id}")]
    public async Task<IActionResult> DeleteCategoryAsync(int id)
    {
        try
        {
            var category = await _categoryService.GetByIdAsync(id);
            await _categoryService.SoftDeleteAsync(category.Id);
            return Ok(new Response {Status = "Success",Message = "Category Deleted Successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpGet]
    [Route("searchCategoryAsync")]
    public async Task<IActionResult> SearchCategoryAsync(string categoryName)
    {
        try
        {
            Expression<Func<Category, bool>> predicate = c => c.CategoryName.ToLower().Contains(categoryName.ToLower());
            var category = await _categoryService.SearchCategoryAsync(predicate);
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
}