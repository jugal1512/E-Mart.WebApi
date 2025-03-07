﻿using E_Mart.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace E_Mart.WebApi.Models.User;

public class UserDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string RoleName { get; set; }
}