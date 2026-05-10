using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPos.Application.Interfaces;
using PizzaPos.Application.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using PizzaPos.Api.Hubs;
using PizzaPos.Application.Common;

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
        return Ok(DynamicResponse<decimal>.CreateSuccess(rate));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var results = await _orderService.GetActiveOrdersAsync();
        return Ok(DynamicResponse<IEnumerable<OrderResponseDto>>.CreateSuccess(results));
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string? searchTerm)
    {
        var results = await _orderService.GetOrderHistoryAsync(startDate, endDate, searchTerm);
        return Ok(DynamicResponse<IEnumerable<OrderResponseDto>>.CreateSuccess(results));
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
            
            return Ok(DynamicResponse<OrderResponseDto>.CreateSuccess(result, "Pedido creado correctamente"));
        }
        catch (Exception ex)
        {
            return BadRequest(DynamicResponse<string>.CreateError(ex.Message));
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
            
            return Ok(DynamicResponse<string>.CreateSuccess("Estado actualizado"));
        }
        catch (Exception ex)
        {
            return BadRequest(DynamicResponse<string>.CreateError(ex.Message));
        }
    }

    [HttpGet("status/{statusCode}")]
    public async Task<IActionResult> GetByStatus(string statusCode)
    {
        var results = await _orderService.GetOrdersByStatusAsync(statusCode);
        return Ok(DynamicResponse<IEnumerable<OrderResponseDto>>.CreateSuccess(results));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _orderService.GetOrderByIdAsync(id);
        if (result == null) return NotFound(DynamicResponse<string>.CreateError("Pedido no encontrado"));
        return Ok(DynamicResponse<OrderResponseDto>.CreateSuccess(result));
    }
}
