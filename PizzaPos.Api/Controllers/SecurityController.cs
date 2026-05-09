using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPos.Application.DTOs;
using PizzaPos.Application.Interfaces;

namespace PizzaPos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class SecurityController : ControllerBase
{
    private readonly ISecurityService _securityService;

    public SecurityController(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    [HttpPost("roles")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        await _securityService.CreateRoleAsync(request);
        return Ok(new { message = "Rol creado exitosamente" });
    }

    [HttpPost("permissions")]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest request)
    {
        await _securityService.CreatePermissionAsync(request);
        return Ok(new { message = "Permiso creado exitosamente" });
    }
}
