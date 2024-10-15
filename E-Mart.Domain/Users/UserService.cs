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

    public async Task<User> UserExistsAsync(string userName)
    {
        return await _userRepository.UserExistsAsync(userName);
    }

    public async Task<User> RegisterUserAsync(User user)
    {
        return await _userRepository.RegisterUserAsync(user);
    }

    public async Task<UserDetails> AddUserAddressAsync(UserDetails userAddress)
    {
        return await _userRepository.AddUserAddressAsync(userAddress);
    }
}
