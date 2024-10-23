using AutoMapper;
using E_Mart.Domain.Categories;
using E_Mart.Utility.FirebaseImageUpload;
using E_Mart.WebApi.Models.Category;
using E_Mart.WebApi.Models.Response;
using E_Mart.WebApi.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace E_Mart.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubCategoryController : ControllerBase
{
    #region "Variable Declarations"
    private readonly CategoryService _categoryService;
    private readonly SubCategoriesService _subCategoryService;
    private readonly FileUploadSettings _fileUploadSettings;
    private readonly IFirebaseImageUploadService _firebaseImageUploadService;
    private readonly IMapper _mapper;
    #endregion
    public SubCategoryController(CategoryService categoryService, SubCategoriesService subCategoryService, IMapper mapper, IOptions<FileUploadSettings> fileUploadSettings, IFirebaseImageUploadService firebaseImageUploadService)
    {
        _categoryService = categoryService;
        _subCategoryService = subCategoryService;
        _fileUploadSettings = fileUploadSettings.Value;
        _firebaseImageUploadService = firebaseImageUploadService;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin,Seller")]
    [HttpPost]
    [Route("createSubCategoryAsync")]
    public async Task<IActionResult> CreateSubCategoryAsync([FromForm] SubCategoryDto subCategoryDto)
    {
        try
        {
            var subCategoryExists = await _subCategoryService.GetSubCategoryByNameAsync(subCategoryDto.Name);
            if (subCategoryExists != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "Sub Category is Already Exists!" });
            }
            var categoryExist = await _categoryService.GetCategoryByNameAsync(subCategoryDto.CategoryName);
            if (categoryExist == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Category is Not Available!" });
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
            return Ok(new Response { Status = "Success", Message = "SubCategory Added Successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPut]
    [Route("updateSubCategoriesAsync")]
    public async Task<IActionResult> UpdateSubCategoriesAsync([FromForm] SubCategoryUpdateViewModal subCategoryDto)
    {
        try
        {
            var subCategoryExist = await _subCategoryService.GetByIdAsync(subCategoryDto.Id);
            if (subCategoryExist == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "SubCategory is Not Found!" });
            }
            var newSubCategoryId = subCategoryExist.ParentCategoryId;
            if (subCategoryDto.CategoryName != null)
            {
                var categoryExists = await _categoryService.GetCategoryByNameAsync(subCategoryDto.CategoryName);
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
            return Ok(new Response { Status = "Success", Message = "SubCategory Updated Successfully." });
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
        try
        {
            var subCategories = await _subCategoryService.GetAllAsync();
            if (subCategories == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "SubCategories are Not found!" });
            }
            var subCategoriesViewModal = _mapper.Map<List<SubCategoriesViewModal>>(subCategories);
            return Ok(subCategoriesViewModal);
        }
        catch (Exception ex)
        {
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

    [HttpGet]
    [Route("searchSubCategoryAsync")]
    public async Task<IActionResult> SearchSubCategoryAsync(string subCategoryName)
    {
        try
        {
            Expression<Func<SubCategories, bool>> predicate = c => c.Name.ToLower().Contains(subCategoryName.ToLower());
            var subCategory = await _subCategoryService.SearchSubCategoryAsync(predicate);
            if (subCategory == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Category Not Found!" });
            }
            return Ok(subCategory);
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
        var fileUploadFolder = _fileUploadSettings.CategoryPage;
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
        return [categoryImageName, downloadUrl];
    }

    private async void DeleteCategoryImage(string oldCategoryImage)
    {
        var fileUploadFolder = _fileUploadSettings.CategoryPage;
        var firebaseGetImage = new FirebaseImageUploadModal
        {
            fileUploadFolder = fileUploadFolder,
            fileName = oldCategoryImage,
        };
        await _firebaseImageUploadService.FirebaseDeleteUploadImageAsync(firebaseGetImage);
    }
}
