using E_Mart.Domain.Customer;

namespace E_Mart.Domain.Users;
public interface IUserService
{
    Task<User> UserExistsAsync(string userName);
    Task<User> RegisterUserAsync(User user);
    Task<UserDetails> AddUserAddressAsync(UserDetails userAddress);
}