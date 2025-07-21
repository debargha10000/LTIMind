using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBite.API.src.models.dto.menuItem;
using QuickBite.API.src.services;

namespace QuickBite.API.src.controllers;

[Route("[controller]")]
[ApiController]
public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
{
    private readonly IMenuItemService _menuItemService = menuItemService;

    [HttpPost]
    public async Task<IActionResult> CreateNewMenuItem([FromBody] MenuItemRequestBodyDTO dto)
    {
        var response = await _menuItemService.CreateNewMenuItem(dto);
        return response.Build();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem([FromRoute] int id, [FromBody] MenuItemRequestBodyDTO dto)
    {
        var response = await _menuItemService.UpdateMenuItem(id, dto);
        return response.Build();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneMenuItemById([FromRoute] int id)
    {
        var response = await _menuItemService.GetOneMenuItemById(id);
        return response.Build();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems()
    {
        var response = await _menuItemService.GetAllMenuItems();
        return response.Build();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItemById([FromRoute] int id)
    {
        var response = await _menuItemService.DeleteMenuItemById(id);
        return response.Build();
    }
}