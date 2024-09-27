using AutoMapper;
using E_Mart.Domain.Users;
using E_Mart.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Mart.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserManagementController : ControllerBase
{
    private readonly RoleService _roleService;
    private readonly UserService _userService;
    private readonly IMapper _mapper;
    public UserManagementController(IMapper mapper,RoleService roleService,UserService userService)
    {
        _mapper = mapper;
        _roleService = roleService;
        _userService = userService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromForm]RoleDto roleDto)
    {
        try {
            var roleModel = _mapper.Map<Role>(roleDto);
            var roleExists = await _roleService.RoleExists(roleModel.RoleName);
            if (roleExists != null)
            {
                return Ok(new Response { Status = "Error", Message = "Role Already Have Exists!" });
            }
            else
            {
                var role = await _roleService.CreateRole(roleModel);
                if (role != null)

                {
                    return Ok(new Response { Status = "Success", Message = "Role Created Is Successfully." });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role Not Created !" });
                }
            }
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserDto userDto)
    {
        try {
            var userExists = await _userService.UserExists(userDto.UserName);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists!" });
            }
            else
            {
                var userRoleExists = await _roleService.RoleExists(userDto.RoleName);
                if (userRoleExists != null)
                {
                       
                }
                else 
                { 
                       
                }
            }
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
        return Ok();
    }
}