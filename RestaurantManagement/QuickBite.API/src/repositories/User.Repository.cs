using Microsoft.EntityFrameworkCore;
using QuickBite.API.src.config;
using QuickBite.API.src.enums;
using QuickBite.API.src.models.entities;

namespace QuickBite.API.src.repositories;

// Interface
public interface IUserRepository : IBaseRepository<UserEntity>
{
    public Task<UserEntity?> FindByEmail(string email);
    public Task<IEnumerable<UserEntity>> FindAllByRole(UserRole role);
}

// Implementing class
public class UserRepository(AppDbContext context) : BaseRepository<UserEntity>(context), IUserRepository
{
    public async Task<UserEntity?> FindByEmail(string email)
    {
        return await _context.Users.Where(u => u.Email == email).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<UserEntity>> FindAllByRole(UserRole role)
    {
        return await _context.Users.Where(u => u.Role == role).ToListAsync();
    }
}