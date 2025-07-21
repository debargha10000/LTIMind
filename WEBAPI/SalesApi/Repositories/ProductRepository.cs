using System;
using SalesApi.Data;
using SalesApi.Models;

namespace SalesApi.Repositories;

public class ProductRepository : RepositoryBase<Product>
{
    public ProductRepository(AppDbContext context) : base(context) { }
}
