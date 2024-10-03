using E_Mart.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace E_Mart.WebApi.Models.User;

public class UserDto
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
}
