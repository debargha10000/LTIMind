using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SalesApi.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required, MaxLength(255)]
    public string ProductName { get; set; } = null!;

    [MaxLength(255)]
    public string Category { get; set; } = "Uncategorized";

    [Precision(10, 2)]
    public decimal Price { get; set; } = decimal.Zero;

    public Product() { }

    public Product(string productName, string category, decimal price)
    {
        ProductName = productName;
        Category = category;
        Price = price;
    }
}
