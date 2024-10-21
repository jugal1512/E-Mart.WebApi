using E_Mart.WebApi.Models.Product;

namespace E_Mart.WebApi.Models.Wishlist;

public class WishlistViewModal
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public virtual ProductViewModal Product { get; set; }

}
