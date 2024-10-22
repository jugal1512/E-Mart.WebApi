using E_Mart.Domain.Base;
using E_Mart.Domain.Products;

namespace E_Mart.Domain.Inventories;
public class Inventory : BaseEntity
{
    public int ProductId { get; set; }
    public int QuantityAvailable { get; set; }
    public int QuantityReserved { get; set; }
    public Product Product { get; set; }
}
