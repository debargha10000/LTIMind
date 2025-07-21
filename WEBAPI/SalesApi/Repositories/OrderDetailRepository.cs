using System;
using SalesApi.Data;
using SalesApi.Models;

namespace SalesApi.Repositories;

public class OrderDetailRepository : RepositoryBase<OrderDetail>
{
    public OrderDetailRepository(AppDbContext context) : base(context) { }
}
