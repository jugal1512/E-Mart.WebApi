﻿namespace E_Mart.WebApi.Models.Category;

public class CategoryViewModal
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public string CategoryImage { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }
}
