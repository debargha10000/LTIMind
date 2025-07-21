using System.Net;
using QuickBite.API.src.models.dto.menuItem;
using QuickBite.API.src.models.entities;
using QuickBite.API.src.repositories;
using QuickBite.API.src.utils;

namespace QuickBite.API.src.services;

// Interface

public interface IMenuItemService
{
    public Task<ResponseEntity<MenuItemEntity>> CreateNewMenuItem(MenuItemRequestBodyDTO dto);
    public Task<ResponseEntity<MenuItemEntity>> UpdateMenuItem(int id, MenuItemRequestBodyDTO dto);
    public Task<ResponseEntity<MenuItemEntity>> GetOneMenuItemById(int id);
    public Task<ResponseEntity<IEnumerable<MenuItemEntity>>> GetAllMenuItems();
    public Task<ResponseEntity<MenuItemEntity>> DeleteMenuItemById(int id);
}

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _repository;

    public MenuItemService(IMenuItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseEntity<MenuItemEntity>> CreateNewMenuItem(MenuItemRequestBodyDTO dto)
    {
        var menuItem = new MenuItemEntity(dto);
        await _repository.Create(menuItem);
        await _repository.SaveChangesAsync();
        return new ResponseEntity<MenuItemEntity>(HttpStatusCode.Created, "Menu item created successfully", menuItem);
    }

    public async Task<ResponseEntity<MenuItemEntity>> UpdateMenuItem(int id, MenuItemRequestBodyDTO dto)
    {
        var menuItem = await _repository.FindById(id);
        if (menuItem == null)
        {
            return new ResponseEntity<MenuItemEntity>(HttpStatusCode.NotFound, "Menu item not found");
        }
        menuItem.Name = dto.Name;
        menuItem.Description = dto.Description;
        menuItem.DietInfo = dto.DietInfo;
        menuItem.Price = dto.Price;
        menuItem.ImageUrl = dto.ImageUrl;
        _repository.Update(menuItem);
        await _repository.SaveChangesAsync();
        return new ResponseEntity<MenuItemEntity>(HttpStatusCode.OK, "Menu item updated successfully", menuItem);
    }

    public async Task<ResponseEntity<MenuItemEntity>> GetOneMenuItemById(int id)
    {
        var menuItem = await _repository.FindById(id);
        if (menuItem == null)
        {
            return new ResponseEntity<MenuItemEntity>(HttpStatusCode.NotFound, "Menu item not found");
        }
        return new ResponseEntity<MenuItemEntity>(HttpStatusCode.OK, "Menu item found successfully", menuItem);
    }

    public async Task<ResponseEntity<IEnumerable<MenuItemEntity>>> GetAllMenuItems()
    {
        var menuItems = await _repository.FindAll();
        return new ResponseEntity<IEnumerable<MenuItemEntity>>(HttpStatusCode.OK, $"{menuItems.Count()} Menu item(s) found successfully", menuItems);
    }

    public async Task<ResponseEntity<MenuItemEntity>> DeleteMenuItemById(int id)
    {
        var menuItem = await _repository.FindById(id);
        if (menuItem == null)
        {
            return new ResponseEntity<MenuItemEntity>(HttpStatusCode.NotFound, "Menu item not found");
        }
        _repository.Delete(menuItem);
        return new ResponseEntity<MenuItemEntity>(HttpStatusCode.OK, "Menu item deleted successfully", menuItem);
    }
}
