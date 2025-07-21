using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuickBite.API.src.enums;
using QuickBite.API.src.models.dto.session;
using QuickBite.API.src.models.dto.user;
using QuickBite.API.src.models.entities;
using QuickBite.API.src.services;
using QuickBite.API.src.utils;

namespace QuickBite.API.src.controllers;

[ApiController]
[Route("user")]
public class User(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("")]
    public async Task<IActionResult> CreateNewUser([FromQuery] UserRole role, [FromBody] UserCreateRequestBodyDTO data)
    {
        if (!ModelState.IsValid)
        {
            return new ResponseEntity<ModelStateDictionary>(HttpStatusCode.BadRequest, "User Creation Failed", false, ModelState).Build();
        }
        ResponseEntity<UserEntity?> response;

        if (role == UserRole.Customer)
            response = await _userService.CreateNewCustomer(data);
        else if (role == UserRole.Admin)
            response = await _userService.CreateNewAdmin(data);
        else
            response = new ResponseEntity<UserEntity?>(HttpStatusCode.BadRequest, "Invalid User Type");
        return response.Build();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserUpdateRequestBodyDTO data)
    {
        if (!ModelState.IsValid)
        {
            return new ResponseEntity<ModelStateDictionary>(HttpStatusCode.BadRequest, "User Update Failed", false, ModelState).Build();
        }
        ResponseEntity<UserEntity?> response = await _userService.UpdateUserById(id, data);
        return response.Build();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] int id)
    {
        ResponseEntity<UserEntity?> response = await _userService.DeleteUserById(id);
        return response.Build();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        ResponseEntity<UserEntity?> response = await _userService.GetOneUserById(id);
        return response.Build();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers([FromQuery] UserRole? role)
    {
        Console.WriteLine(role);
        ResponseEntity<IEnumerable<UserEntity>> response;
        if (role != null && Enum.IsDefined(typeof(UserRole), role))
        {
            Console.WriteLine("one");
            response = await _userService.GetAllUsers(role.GetValueOrDefault());
        }
        else
        {
            Console.WriteLine("two");
            response = await _userService.GetAllUsers();
        }
        return response.Build();
    }

    [HttpPost("auth/login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestBodyDTO data)
    {
        ResponseEntity<SessionResponseDTO> response = await _userService.LoginUser(data);
        return response.Build();
    }

    [HttpPost("auth/logout")]
    public async Task<IActionResult> LogoutUser([FromQuery] SessionRemoveRequestBodyDTO data)
    {
        if (!ModelState.IsValid)
        {
            return new ResponseEntity<ModelStateDictionary>(HttpStatusCode.BadRequest, "User Logout Failed", false, ModelState).Build();
        }
        ResponseEntity<SessionResponseDTO> response = await _userService.LogoutUser(data);
        return response.Build();
    }

    [HttpPost("auth/refresh")]
    public async Task<IActionResult> RefreshUserSession([FromQuery] SessionRefreshRequestBodyDTO data)
    {
        if (!ModelState.IsValid)
        {
            return new ResponseEntity<ModelStateDictionary>(HttpStatusCode.BadRequest, "Session Validation Failed", false, ModelState).Build();
        }
        ResponseEntity<SessionResponseDTO> response = await _userService.RefreshUserSession(data);
        return response.Build();
    }
}
