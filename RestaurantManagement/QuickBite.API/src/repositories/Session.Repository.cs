using Microsoft.EntityFrameworkCore;
using QuickBite.API.src.config;
using QuickBite.API.src.models.entities;

namespace QuickBite.API.src.repositories;

// Interface
public interface ISessionRepository : IBaseRepository<SessionEntity>
{
    public Task<SessionEntity?> FindByToken(string refreshToken);
}

// Implementing class
public class SessionRepository(AppDbContext context) : BaseRepository<SessionEntity>(context), ISessionRepository
{
    public async Task<SessionEntity?> FindByToken(string token)
    {
        return await _context.Sessions.Where(x => x.Token == token).SingleOrDefaultAsync();
    }
}
