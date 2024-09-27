using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Users;
public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<Role>> GetRoles()
    {
        return await _roleRepository.GetRoles();
    }

    public async Task<Role> RoleExists(string roleName)
    {
        return await _roleRepository.RoleExists(roleName);
    }

    public async Task<Role> CreateRole(Role role)
    {
        return await _roleRepository.CreateRole(role);   
    }
}
