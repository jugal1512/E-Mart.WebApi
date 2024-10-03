﻿using E_Mart.Domain.Categories;

namespace E_Mart.WebApi.Models.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
    public int? CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<IFormFile> ProductImage { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

}