﻿using E_Mart.Domain.Products;
using E_Mart.Domain.Sellers;

namespace E_Mart.Domain.OrderDetails;
public class OrderItem 
{
    public int Id { get; set; }
    public int orderId { get; set; }
    public int ProductId { get; set; }
    public int SellerId { get; set; }
    public int Quantity { get; set; }
    public virtual OrderDetails OrderDetails { get; set; }
    public virtual Product Product { get; set; }
    public virtual Seller Seller { get; set; }
}