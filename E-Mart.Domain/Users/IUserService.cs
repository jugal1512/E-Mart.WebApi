using E_Mart.Domain.Customer;

namespace E_Mart.Domain.Users;
public interface IUserService
{
    Task<User> UserExists(string userName);
    Task<User> RegisterUser(User user);
    Task<UserDetails> AddUserAddress(UserDetails userAddress);
}