using E_Mart.Domain.Customer;
using E_Mart.Domain.Users;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Repositories;
public class UserRepository : IUserRepository
{
    private readonly EMartDbContext _eMartDbContext;
    public UserRepository(EMartDbContext eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
    }

    public async Task<User> UserExists(string userName)
    {
        var user = await _eMartDbContext.Users.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
        return user;
    }

    public async Task<User> RegisterUser(User user)
    {
        await _eMartDbContext.AddAsync(user);
        await _eMartDbContext.SaveChangesAsync();
        return user;
    }

}
