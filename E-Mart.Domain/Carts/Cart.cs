using E_Mart.Domain.Base;
using E_Mart.Domain.Customer;
using E_Mart.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Carts;
public class Cart : BaseEntity
{
    public int UserId { get; set; }
    public double Total { get; set; }
    public virtual User User { get; set; }
    public ICollection<Cart_Item> Cart_Items { get; set; }
}