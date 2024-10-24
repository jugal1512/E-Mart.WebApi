﻿using E_Mart.Domain.Base;
using E_Mart.Domain.Products;

namespace E_Mart.Domain.Carts;
public class CartItem 
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int ProductPrice { get; set; }
    public int Quantity { get; set; }
    public virtual Cart Cart { get; set; }
    public virtual Product Product { get; set; }
}