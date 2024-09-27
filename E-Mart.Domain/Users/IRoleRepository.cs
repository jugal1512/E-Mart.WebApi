using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Users;
public interface IRoleRepository
{
    Task<List<Role>> GetRoles();
    Task<Role> RoleExists(string roleName);
    Task<Role> CreateRole(Role role);
}
