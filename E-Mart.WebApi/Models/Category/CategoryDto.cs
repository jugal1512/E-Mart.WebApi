﻿using E_Mart.Domain.Products;

namespace E_Mart.WebApi.Models.Category;

public class CategoryDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
}