using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SalesApi.DTOs.Input;
using SalesApi.Services;

namespace SalesApi.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;
    public DateOnly OrderDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public Order() { }

    public Order(int customerId, DateOnly orderDate)
    {
        CustomerId = customerId;
        OrderDate = orderDate;
    }

    public Order(OrderInputDto input)
    {
        CustomerId = input.CustomerId;
        OrderDate = input.OrderDate;

        CustomerService customerService = new();
        // change the Customer object to the one with the new id
        Customer = GetByIdAsync(input.CustomerId).Result;
    }
}
