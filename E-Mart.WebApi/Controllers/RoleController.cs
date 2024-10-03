using AutoMapper;
using E_Mart.Domain.Users;
using E_Mart.WebApi.Models;
using E_Mart.WebApi.Models.Response;
using E_Mart.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Mart.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;
    private readonly IMapper _mapper;
    public RoleController(RoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("CreateRole")]
    public async Task<IActionResult> CreateRole([FromForm] RoleDto roleDto)
    {
        try
        {
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
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpGet]
    [Route("GetAllRoles")]
    public async Task<IActionResult> GetAllRoles()
    {
        try
        {
            string userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var roles = await _roleService.GetRoles();
            if (roles == null)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role Not Created !" });
            }
            return Ok(roles);
        }
        catch (Exception ex) {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

}
