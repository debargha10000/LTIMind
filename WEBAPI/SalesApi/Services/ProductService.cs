using System;
using SalesApi.Models;
using SalesApi.Repositories;

namespace SalesApi.Services;

public class ProductService
{
    private readonly ProductRepository _repo;
    public ProductService(ProductRepository repo) => _repo = repo;

    public Task<IEnumerable<Product>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Product?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<Product> AddAsync(Product entity) => _repo.AddAsync(entity);
    public Task UpdateAsync(Product entity) => _repo.UpdateAsync(entity);
    public Task DeleteAsync(Product entity) => _repo.DeleteAsync(entity);
}
