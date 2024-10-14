using AutoMapper;
using E_Mart.Domain.Authentication;
using E_Mart.Domain.Categories;
using E_Mart.Domain.Customer;
using E_Mart.Domain.Products;
using E_Mart.Domain.Users;
using E_Mart.WebApi.Models.Authentication;
using E_Mart.WebApi.Models.Category;
using E_Mart.WebApi.Models.Product;
using E_Mart.WebApi.Models.User;

namespace E_Mart.WebApi.AutoMapper;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateRoleMaps();
        CreateUserMaps();
        CreateCategoryMaps();
        CreateProductMaps();
    }

    private void CreateRoleMaps()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
    }

    private void CreateUserMaps()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Login, LoginDto>().ReverseMap();
        CreateMap<UserDetails, UserDetailsDto>().ReverseMap();
    }

    private void CreateCategoryMaps()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryViewModal>().ReverseMap();
        CreateMap<SubCategories, SubCategoryDto>().ReverseMap();
        CreateMap<SubCategories, SubCategoriesViewModal>().ReverseMap();
        CreateMap<SubCategories, SubCategoryUpdateViewModal>().ReverseMap();
    }


    private void CreateProductMaps()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductViewModal>().ReverseMap();
        CreateMap<Product, ProductUpdateViewModal>().ReverseMap();
    }
}
