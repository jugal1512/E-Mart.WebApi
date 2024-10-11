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
    private readonly SubCategoriesService _subCategoryService;
    private readonly string _fileUploadFolder;
    private readonly IFirebaseImageUploadService _firebaseImageUploadService;
    private readonly IMapper _mapper;
    public CategoryController(CategoryService categoryService, SubCategoriesService subCategoryService, IMapper mapper, IConfiguration configuration, IFirebaseImageUploadService firebaseImageUploadService)
    {
        _categoryService = categoryService;
        _subCategoryService = subCategoryService;
        _fileUploadFolder = configuration["FileUploadSettingds:CategoryPage"];
        _firebaseImageUploadService = firebaseImageUploadService;
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
            var categoryExists = await _categoryService.GetCategoryByName(categoryDto.CategoryName);
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

    [Authorize(Roles = "Admin,Seller")]
    [HttpPost]
    [Route("createSubCategoryAsync")]
    public async Task<IActionResult> CreateSubCategoryAsync([FromForm]SubCategoryDto subCategoryDto)
    {
        try
        {
            var categoryExist = await _categoryService.GetCategoryByName(subCategoryDto.CategoryName);
            if (categoryExist == null) {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error",Message = "Category is Not Available!" });
            }
            if (subCategoryDto.CategoryName == null || subCategoryDto.CategoryName.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            List<string> uploadImage = await saveCategoryImageAsync(subCategoryDto.CategoryImage);
            var categoryImageName = uploadImage[0];
            var subCategory = _mapper.Map<SubCategories>(subCategoryDto);
            subCategory.ParentCategoryId = categoryExist.Id;
            subCategory.CategoryImage = categoryImageName;
            var addSubCategory = await _subCategoryService.AddAsync(subCategory);
            return Ok(new Response { Status = "Success",Message = "SubCategory Added Successfully."});
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPut]
    [Route("updateSubCategoriesAsync")]
    public async Task<IActionResult> UpdateSubCategoriesAsync([FromForm]SubCategoryUpdateViewModal subCategoryDto)
    {
        try
        {
            var subCategoryExist = await _subCategoryService.GetByIdAsync(subCategoryDto.Id);
            if (subCategoryExist == null )
            {
                return StatusCode(StatusCodes.Status404NotFound,new Response { Status = "Error",Message = "SubCategory is Not Found!"});
            }
            var newSubCategoryId = subCategoryExist.ParentCategoryId;
            if (subCategoryDto.CategoryName != null)
            {
                var categoryExists = await _categoryService.GetCategoryByName(subCategoryDto.CategoryName);
                if (categoryExists != null)
                {
                    newSubCategoryId = categoryExists.Id;
                }
            }
            var categoryImageName = subCategoryExist.CategoryImage;
            if (subCategoryDto.CategoryImage != null)
            {
                DeleteCategoryImage(subCategoryExist.CategoryImage);
                List<string> uploadImage = await saveCategoryImageAsync(subCategoryDto.CategoryImage);
                categoryImageName = uploadImage[0];
            }
            var subCategory = _mapper.Map<SubCategories>(subCategoryDto);
            subCategory.CreatedAt = subCategoryExist.CreatedAt;
            subCategory.ParentCategoryId = newSubCategoryId;
            subCategory.CategoryImage = categoryImageName;
            var updateSubCategory = await _subCategoryService.UpdateAsync(subCategory);
            return Ok(new Response { Status = "Success",Message = "SubCategory Updated Successfully."});
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpGet]
    [Route("getSubCategoriesAsync")]
    public async Task<IActionResult> GetSubCategoriesAsync()
    {
        try {
            var subCategories = await _subCategoryService.GetAllAsync();
            if (subCategories == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "SubCategories are Not found!" });
            }
            var subCategoriesViewModal = _mapper.Map<List<SubCategoriesViewModal>>(subCategories);
            return Ok(subCategoriesViewModal);
        }
        catch(Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete] 
    [Route("deleteSubCategoryAsync/{id}")]
    public async Task<IActionResult> DeleteSubCategoryAsync(int id)
    {
        try
        {
            var subCategory = await _subCategoryService.GetByIdAsync(id);
            DeleteCategoryImage(subCategory.CategoryImage);
            await _subCategoryService.SoftDeleteAsync(subCategory.Id);
            return Ok(new Response { Status = "Success", Message = "Sub Category Deleted Successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    private async Task<List<string>> saveCategoryImageAsync(IFormFile categoryImage)
    {
        var categoryImageName = Guid.NewGuid().ToString() + "_" + categoryImage.FileName;
        var filePath = Path.Combine(Path.GetTempPath(), categoryImageName);
        var fileUploadFolder = _fileUploadFolder;
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await categoryImage.CopyToAsync(stream);
        }
        var firebaseImageUpload = new FirebaseImageUploadModal
        {
            fileUploadFolder = fileUploadFolder,
            fileName = categoryImageName,
            filePath = filePath,
        };
        var downloadUrl = await _firebaseImageUploadService.FirebaseUploadImageAsync(firebaseImageUpload);
        return [categoryImageName,downloadUrl];
    }

    private async void DeleteCategoryImage(string oldCategoryImage)
    {
        var fileUploadFolder = _fileUploadFolder;
        var firebaseGetImage = new FirebaseImageUploadModal
        {
            fileUploadFolder = fileUploadFolder,
            fileName = oldCategoryImage,
        };
        await _firebaseImageUploadService.FirebaseDeleteUploadImageAsync(firebaseGetImage);
    }
}