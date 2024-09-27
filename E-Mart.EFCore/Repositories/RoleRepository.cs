using E_Mart.Domain.Users;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.EFCore.Repositories;
public class RoleRepository : IRoleRepository
{
    private readonly EMartDbContext _eMartDbContext;
    public RoleRepository(EMartDbContext eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
    }

    public async Task<List<Role>> GetRoles()
    {
        var roles = await _eMartDbContext.Roles.AsNoTracking().ToListAsync();
        return roles;
    }

    public async Task<Role> RoleExists(string roleName)
    {
        var role = await _eMartDbContext.Roles.Where(r => r.RoleName.ToLower() == roleName.ToLower()).FirstOrDefaultAsync();
        return role;
    }

    public async Task<Role> CreateRole(Role role)
    {
        await _eMartDbContext.AddAsync(role);
        await _eMartDbContext.SaveChangesAsync();
        return role;
    }
}
