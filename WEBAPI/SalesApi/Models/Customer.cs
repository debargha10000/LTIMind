using System;
using System.ComponentModel.DataAnnotations;
using SalesApi.DTOs.Input;

namespace SalesApi.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required, MaxLength(255)]
    public string CustomerName { get; set; } = null!;

    [MaxLength(255)]
    public string? City { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public Customer() { }

    public Customer(string customerName, string? city)
    {
        CustomerName = customerName;
        City = city;
    }

    public Customer(CustomerInputDto input)
    {
        CustomerName = input.Name;
        City = input.City;
    }
}