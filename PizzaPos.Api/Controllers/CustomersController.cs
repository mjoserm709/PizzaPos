using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using System.Security.Claims;

namespace PizzaPos.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomersController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        var results = await _customerRepository.SearchAsync(term);
        return Ok(new { success = true, data = results });
    }

    [HttpGet("phone/{phone}")]
    public async Task<IActionResult> GetByPhone(string phone)
    {
        var result = await _customerRepository.GetByPhoneAsync(phone);
        if (result == null) return NotFound(new { success = false, message = "Cliente no encontrado" });
        return Ok(new { success = true, data = result });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Customer customer)
    {
        var currentEmail = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        try
        {
            customer.CreatedAt = DateTime.Now;
            customer.CreatedBy = currentEmail;
            await _customerRepository.AddAsync(customer);
            return Ok(new { success = true, message = "Cliente registrado correctamente", data = customer });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}
