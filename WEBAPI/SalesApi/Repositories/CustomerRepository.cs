using System;
using SalesApi.Data;
using SalesApi.Models;

namespace SalesApi.Repositories;

public class CustomerRepository : RepositoryBase<Customer>
{
    public CustomerRepository(AppDbContext context) : base(context) { }
}
