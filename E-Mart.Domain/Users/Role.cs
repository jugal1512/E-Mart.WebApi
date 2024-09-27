using E_Mart.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Users;
public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; }
    public virtual ICollection<User> Users { get; set; }
}