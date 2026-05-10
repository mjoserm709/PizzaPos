using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPos.Application.DTOs;
using PizzaPos.Application.Interfaces;
using PizzaPos.Application.Common;

namespace PizzaPos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "admin,Admin")]
public class SecurityController : ControllerBase
{
    private readonly ISecurityService _securityService;

    public SecurityController(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _securityService.GetAllRolesAsync();
        return Ok(DynamicResponse<IEnumerable<RoleResponseDto>>.CreateSuccess(roles));
    }

    [HttpPost("roles")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        await _securityService.CreateRoleAsync(request, User.Identity?.Name ?? "System");
        return Ok(DynamicResponse<string>.CreateSuccess("Rol creado exitosamente"));
    }

    [HttpPut("roles")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest request)
    {
        await _securityService.UpdateRoleAsync(request, User.Identity?.Name ?? "System");
        return Ok(DynamicResponse<string>.CreateSuccess("Rol actualizado exitosamente"));
    }

    [HttpPatch("roles/status")]
    public async Task<IActionResult> UpdateRoleStatus([FromBody] UpdateRoleStatusRequest request)
    {
        await _securityService.UpdateRoleStatusAsync(request, User.Identity?.Name ?? "System");
        return Ok(DynamicResponse<string>.CreateSuccess("Estado de rol actualizado"));
    }

    [HttpGet("permissions")]
    public async Task<IActionResult> GetPermissions()
    {
        var perms = await _securityService.GetAllPermissionsAsync();
        return Ok(DynamicResponse<IEnumerable<PermissionResponseDto>>.CreateSuccess(perms));
    }

    [HttpPost("permissions")]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest request)
    {
        await _securityService.CreatePermissionAsync(request, User.Identity?.Name ?? "System");
        return Ok(DynamicResponse<string>.CreateSuccess("Permiso creado exitosamente"));
    }

    [HttpPut("permissions")]
    public async Task<IActionResult> UpdatePermission([FromBody] UpdatePermissionRequest request)
    {
        await _securityService.UpdatePermissionAsync(request, User.Identity?.Name ?? "System");
        return Ok(DynamicResponse<string>.CreateSuccess("Permiso actualizado exitosamente"));
    }

    [HttpPatch("permissions/status")]
    public async Task<IActionResult> UpdatePermissionStatus([FromBody] UpdatePermissionStatusRequest request)
    {
        await _securityService.UpdatePermissionStatusAsync(request, User.Identity?.Name ?? "System");
        return Ok(DynamicResponse<string>.CreateSuccess("Estado de permiso actualizado"));
    }
}
