using System;
using SalesApi.Models;
using SalesApi.Repositories;

namespace SalesApi.Services;

public class OrderService
{
    private readonly OrderRepository _repo;
    public OrderService(OrderRepository repo) => _repo = repo;

    public Task<IEnumerable<Order>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Order?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<Order> AddAsync(Order entity) => _repo.AddAsync(entity);
    public Task UpdateAsync(Order entity) => _repo.UpdateAsync(entity);
    public Task DeleteAsync(Order entity) => _repo.DeleteAsync(entity);
}
