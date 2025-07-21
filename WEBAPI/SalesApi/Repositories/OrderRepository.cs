using System;
using SalesApi.Data;
using SalesApi.Models;

namespace SalesApi.Repositories;

public class OrderRepository : RepositoryBase<Order>
{
    public OrderRepository(AppDbContext context) : base(context) { }
}
