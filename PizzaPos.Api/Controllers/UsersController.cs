using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPos.Application.DTOs;
using PizzaPos.Application.Interfaces;
using PizzaPos.Application.Common;

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
        var users = await _userService.GetAllUsersAsync();
        return Ok(DynamicResponse<IEnumerable<UserResponseDto>>.CreateSuccess(users));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        try
        {
            await _userService.CreateUserAsync(request, User.Identity?.Name ?? "System");
            return Ok(DynamicResponse<string>.CreateSuccess("Usuario creado exitosamente"));
        }
        catch (Exception ex)
        {
            return BadRequest(DynamicResponse<string>.CreateError(ex.Message));
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        try
        {
            await _userService.UpdateUserAsync(request, User.Identity?.Name ?? "System");
            return Ok(DynamicResponse<string>.CreateSuccess("Usuario actualizado exitosamente"));
        }
        catch (Exception ex)
        {
            return BadRequest(DynamicResponse<string>.CreateError(ex.Message));
        }
    }

    [HttpPatch("status")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateUserStatusRequest request)
    {
        await _userService.UpdateStatusAsync(request);
        return Ok(DynamicResponse<string>.CreateSuccess("Estado actualizado"));
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _userService.GetRolesAsync();
        return Ok(DynamicResponse<IEnumerable<string>>.CreateSuccess(roles));
    }

    [HttpGet("permissions")]
    public async Task<IActionResult> GetPermissions()
    {
        var perms = await _userService.GetPermissionsAsync();
        return Ok(DynamicResponse<IEnumerable<string>>.CreateSuccess(perms));
    }
}
