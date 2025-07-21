using System;
using SalesApi.Models;
using SalesApi.Repositories;

namespace SalesApi.Services;

public class CustomerService
{
    private readonly IRepository<Customer> _repo;
    public CustomerService(IRepository<Customer> repo) => _repo = repo;

    public Task<IEnumerable<Customer>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Customer?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<Customer> AddAsync(Customer entity) => _repo.AddAsync(entity);
    public Task UpdateAsync(Customer entity) => _repo.UpdateAsync(entity);
    public Task DeleteAsync(Customer entity) => _repo.DeleteAsync(entity);
}
