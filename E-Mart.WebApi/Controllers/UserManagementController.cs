﻿using AutoMapper;
using E_Mart.Domain.Customer;
using E_Mart.Domain.Users;
using E_Mart.WebApi.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

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
    [Route("RegisterUser")]
    public async Task<IActionResult> RegisterUser(UserDto userDto)
    {
        try
        {
            var userExists = await _userService.UserExists(userDto.UserName);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists!" });
            }
            else
            {
                var userRoleExists = await _roleService.RoleExists(userDto.RoleName);
                if (userRoleExists == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role doesn't Exists!" });
                }
                userDto.RoleId = userRoleExists.Id;
                var PasswordHash = await HashPasword(userDto.PasswordHash);
                userDto.PasswordHash = PasswordHash;
                var user = _mapper.Map<User>(userDto);
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = null;
                var addUser = await _userService.RegisterUser(user);
                if (addUser != null)
                {
                    return Ok(new Response { Status = "Success", Message = "User Created Is Successfully." });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User can not Created!" });
                }                
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    private async Task<string> HashPasword(string password)
    { 
        byte[] salt = new byte[128/8];
        using (var rng = RandomNumberGenerator.Create())
        { 
            rng.GetBytes(salt);
        }

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf:KeyDerivationPrf.HMACSHA256,
                iterationCount:100000,
                numBytesRequested:256/8
            ));
        return $"{Convert.ToBase64String(salt)}:{hashed}";
    }
}