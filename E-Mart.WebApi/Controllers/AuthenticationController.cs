using AutoMapper;
using E_Mart.Domain.Users;
using E_Mart.WebApi.Models;
using E_Mart.WebApi.Models.Authentication;
using E_Mart.WebApi.Models.Response;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Mart.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserService _userService;
    private readonly RoleService _roleService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public AuthenticationController(UserService userService,RoleService roleService,IConfiguration configuration,IMapper mapper)
    {
        _userService = userService;
        _roleService = roleService;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginDto userDto)
    {
        try {
            var userExists = await _userService.UserExists(userDto.UserName);
            if (userExists != null && await VerifyPassword(userExists.PasswordHash, userDto.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userDto.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoleExist = await _roleService.RoleExists(userDto.RoleName);
                if (userRoleExist == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role deesn't Exists!" });
                }
                authClaims.Add(new Claim(ClaimTypes.Role, userRoleExist.RoleName));

                var token = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        catch (Exception ex) { 
            return StatusCode(StatusCodes.Status500InternalServerError,new Response { Status = "Error",Message = ex.Message});
        }
    }

    private async Task<bool> VerifyPassword(string passwordHash, string providedPassword)
    {
        var parts = passwordHash.Split(':');
        if (parts.Length != 2)
        {
            return false;
        }
        var salt = Convert.FromBase64String(parts[0]);
        var hash = parts[1];

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: providedPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return hash == hashed;
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        return token;
    }
}
