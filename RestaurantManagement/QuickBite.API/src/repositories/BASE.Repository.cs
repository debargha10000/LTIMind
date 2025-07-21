using Microsoft.EntityFrameworkCore;
using QuickBite.API.src.config;

namespace QuickBite.API.src.repositories;

public interface IBaseRepository<T> where T : class
{
    public Task Create(T data);
    public Task<T?> FindById(int id);
    public Task<IEnumerable<T>> FindAll();
    public void Update(T data);
    public void Delete(T data);
    public Task SaveChangesAsync();
}

public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task Create(T data) => await _dbSet.AddAsync(data);

    public async Task<T?> FindById(int id) => await _dbSet.FindAsync(id);
    public async Task<IEnumerable<T>> FindAll() => await _dbSet.ToListAsync();

    public void Update(T data) => _dbSet.Update(data);
    public void Delete(T data) => _dbSet.Remove(data);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
