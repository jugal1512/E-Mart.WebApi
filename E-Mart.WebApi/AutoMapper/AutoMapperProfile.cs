using AutoMapper;
using E_Mart.Domain.Authentication;
using E_Mart.Domain.Categories;
using E_Mart.Domain.Customer;
using E_Mart.Domain.Users;
using E_Mart.WebApi.Models;

namespace E_Mart.WebApi.AutoMapper;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Role,RoleDto>().ReverseMap();
        CreateMap<User,UserDto>().ReverseMap();
        CreateMap<Login,LoginDto>().ReverseMap();
        CreateMap<Category,CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryViewModal>().ReverseMap();
    }
}
