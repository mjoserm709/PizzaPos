using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPos.Application.Interfaces;
using PizzaPos.Application.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using PizzaPos.Api.Hubs;

namespace PizzaPos.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IHubContext<OrderHub> _hubContext;

    public OrdersController(IOrderService orderService, IHubContext<OrderHub> hubContext)
    {
        _orderService = orderService;
        _hubContext = hubContext;
    }

    [HttpGet("iva-rate")]
    public async Task<IActionResult> GetIvaRate()
    {
        var rate = await _orderService.GetIvaRateAsync();
        return Ok(new { success = true, rate });
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var results = await _orderService.GetActiveOrdersAsync();
        return Ok(new { success = true, data = results });
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string? searchTerm)
    {
        var results = await _orderService.GetOrderHistoryAsync(startDate, endDate, searchTerm);
        return Ok(new { success = true, data = results });
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var currentEmail = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        try
        {
            var result = await _orderService.CreateOrderAsync(request, currentEmail);
            
            // Notificación en tiempo real
            await _hubContext.Clients.All.SendAsync("NewOrderReceived", result);
            
            return Ok(new { success = true, message = "Pedido creado correctamente", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string statusCode)
    {
        var currentEmail = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        try
        {
            await _orderService.UpdateOrderStatusAsync(id, statusCode, currentEmail);
            
            // Notificación en tiempo real
            await _hubContext.Clients.All.SendAsync("OrderStatusChanged", new { OrderId = id, Status = statusCode });
            
            return Ok(new { success = true, message = "Estado actualizado" });
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
