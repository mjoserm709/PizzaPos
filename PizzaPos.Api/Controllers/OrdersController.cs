using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPos.Application.Interfaces;
using PizzaPos.Application.DTOs;
using System.Security.Claims;

namespace PizzaPos.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("iva-rate")]
    public async Task<IActionResult> GetIvaRate()
    {
        var rate = await _orderService.GetIvaRateAsync();
        return Ok(new { success = true, rate });
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var currentEmail = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        try
        {
            var result = await _orderService.CreateOrderAsync(request, currentEmail);
            return Ok(new { success = true, message = "Pedido creado correctamente", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    [HttpGet("status/{statusCode}")]
    public async Task<IActionResult> GetByStatus(string statusCode)
    {
        var results = await _orderService.GetOrdersByStatusAsync(statusCode);
        return Ok(new { success = true, data = results });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _orderService.GetOrderByIdAsync(id);
        if (result == null) return NotFound(new { success = false, message = "Pedido no encontrado" });
        return Ok(new { success = true, data = result });
    }
}
