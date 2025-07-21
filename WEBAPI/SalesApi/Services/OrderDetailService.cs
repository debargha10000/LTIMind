using System;
using SalesApi.Models;
using SalesApi.Repositories;

namespace SalesApi.Services;

public class OrderDetailService
{
    private readonly OrderDetailRepository _repo;
    public OrderDetailService(OrderDetailRepository repo) => _repo = repo;

    public Task<IEnumerable<OrderDetail>> GetAllAsync() => _repo.GetAllAsync();
    public Task<OrderDetail?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<OrderDetail> AddAsync(OrderDetail entity) => _repo.AddAsync(entity);
    public Task UpdateAsync(OrderDetail entity) => _repo.UpdateAsync(entity);
    public Task DeleteAsync(OrderDetail entity) => _repo.DeleteAsync(entity);
}
