using Microsoft.EntityFrameworkCore;
using QuickBite.API.src.config;
using QuickBite.API.src.models.entities;

namespace QuickBite.API.src.repositories;

public interface IMenuItemRepository : IBaseRepository<MenuItemEntity>
{

}
public class MenuItemRepository(AppDbContext context) : BaseRepository<MenuItemEntity>(context), IMenuItemRepository
{

}
