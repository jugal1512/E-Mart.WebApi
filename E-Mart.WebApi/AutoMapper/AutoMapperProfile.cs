using AutoMapper;
using E_Mart.Domain.Users;
using E_Mart.WebApi.Models;

namespace E_Mart.WebApi.AutoMapper;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Role,RoleDto>().ReverseMap();
    }
}
