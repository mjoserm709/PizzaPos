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
    private readonly ICompensationRepository _compensationRepository;

    public CustomersController(ICustomerRepository customerRepository, ICompensationRepository compensationRepository)
    {
        _customerRepository = customerRepository;
        _compensationRepository = compensationRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _customerRepository.GetAllAsync();
        return Ok(new { success = true, data = results });
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
            
            foreach (var addr in customer.Addresses)
            {
                addr.CreatedAt = DateTime.Now;
                addr.CreatedBy = currentEmail;
            }
            
            await _customerRepository.AddAsync(customer);
            return Ok(new { success = true, message = "Cliente registrado correctamente", data = customer });
        }
        catch (Exception ex)
        {
            var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return BadRequest(new { success = false, message = message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
    {
        try
        {
            var existing = await _customerRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { success = false, message = "Cliente no encontrado" });

            existing.FullName = customer.FullName;
            existing.Phone = customer.Phone;
            existing.Email = customer.Email;
            
            // Sincronizar direcciones y auditoría
            foreach (var addr in customer.Addresses)
            {
                if (addr.Id == 0)
                {
                    addr.CreatedAt = DateTime.Now;
                    addr.CreatedBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                }
            }
            existing.Addresses = customer.Addresses;
            
            await _customerRepository.UpdateAsync(existing);
            return Ok(new { success = true, message = "Cliente y direcciones actualizados correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        try
        {
            var existing = await _customerRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { success = false, message = "Cliente no encontrado" });

            existing.IsActive = !existing.IsActive;
            await _customerRepository.UpdateAsync(existing);
            return Ok(new { success = true, message = $"Cliente {(existing.IsActive ? "activado" : "desactivado")} correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
    [HttpGet("{id}/compensation")]
    public async Task<IActionResult> GetPendingCompensation(int id)
    {
        var compensation = await _compensationRepository.GetPendingByCustomerIdAsync(id);
        if (compensation == null) return Ok(new { success = true, data = (object?)null });
        
        return Ok(new { success = true, data = compensation });
    }

    [HttpPost("{id}/redeem-compensation")]
    public async Task<IActionResult> RedeemCompensation(int id)
    {
        try
        {
            var compensation = await _compensationRepository.GetPendingByCustomerIdAsync(id);
            if (compensation == null) return NotFound(new { success = false, message = "No hay compensaciones pendientes" });

            compensation.IsRedeemed = true;
            compensation.RedeemedAt = DateTime.Now;
            await _compensationRepository.UpdateAsync(compensation);

            return Ok(new { success = true, message = "Compensación canjeada correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}
