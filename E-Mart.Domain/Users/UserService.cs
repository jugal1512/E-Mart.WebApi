using E_Mart.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Users;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> UserExists(string userName)
    {
        return await _userRepository.UserExists(userName);
    }

    public async Task<User> RegisterUser(User user)
    {
        return await _userRepository.RegisterUser(user);
    }
}
