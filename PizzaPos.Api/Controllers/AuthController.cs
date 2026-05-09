using Microsoft.AspNetCore.Mvc;
using PizzaPos.Application.DTOs;
using PizzaPos.Application.Interfaces;

using PizzaPos.Application.Common;

namespace PizzaPos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginService _loginService;

    public AuthController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _loginService.LoginAsync(request);

        if (result == null)
        {
            return Unauthorized(DynamicResponse<LoginResponse>.CreateError("Usuario o contraseña incorrectos"));
        }

        return Ok(DynamicResponse<LoginResponse>.CreateSuccess(result));
    }
}
