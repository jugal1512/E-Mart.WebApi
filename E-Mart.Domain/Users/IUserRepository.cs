using E_Mart.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Users;
public interface IUserRepository
{
    Task<User> UserExistsAsync(string userName);
    Task<User> RegisterUserAsync(User user);
    Task<UserDetails> AddUserAddressAsync(UserDetails userAddress);
}
