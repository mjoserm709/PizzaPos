using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPos.Application.DTOs;
using PizzaPos.Application.Interfaces;

namespace PizzaPos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        try
        {
            await _userService.CreateUserAsync(request, User.Identity?.Name ?? "System");
            return Ok(new { message = "Usuario creado exitosamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        try
        {
            await _userService.UpdateUserAsync(request, User.Identity?.Name ?? "System");
            return Ok(new { message = "Usuario actualizado exitosamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("status")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateUserStatusRequest request)
    {
        await _userService.UpdateStatusAsync(request);
        return Ok(new { message = "Estado actualizado" });
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await _userService.GetRolesAsync());
    }

    [HttpGet("permissions")]
    public async Task<IActionResult> GetPermissions()
    {
        return Ok(await _userService.GetPermissionsAsync());
    }
}
