using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using System.Security.Claims;
using PizzaPos.Application.Common;

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
        return Ok(DynamicResponse<IEnumerable<Customer>>.CreateSuccess(results));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        var results = await _customerRepository.SearchAsync(term);
        return Ok(DynamicResponse<IEnumerable<Customer>>.CreateSuccess(results));
    }

    [HttpGet("phone/{phone}")]
    public async Task<IActionResult> GetByPhone(string phone)
    {
        var result = await _customerRepository.GetByPhoneAsync(phone);
        if (result == null) return NotFound(DynamicResponse<string>.CreateError("Cliente no encontrado"));
        return Ok(DynamicResponse<Customer>.CreateSuccess(result));
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
            return Ok(DynamicResponse<Customer>.CreateSuccess(customer, "Cliente registrado correctamente"));
        }
        catch (Exception ex)
        {
            var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return BadRequest(DynamicResponse<string>.CreateError(message));
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
            return Ok(DynamicResponse<string>.CreateSuccess("Cliente y direcciones actualizados correctamente"));
        }
        catch (Exception ex)
        {
            return BadRequest(DynamicResponse<string>.CreateError(ex.Message));
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
            return Ok(DynamicResponse<string>.CreateSuccess($"Cliente {(existing.IsActive ? "activado" : "desactivado")} correctamente"));
        }
        catch (Exception ex)
        {
            return BadRequest(DynamicResponse<string>.CreateError(ex.Message));
        }
    }
    [HttpGet("{id}/compensation")]
    public async Task<IActionResult> GetPendingCompensation(int id)
    {
        var compensation = await _compensationRepository.GetPendingByCustomerIdAsync(id);
        return Ok(DynamicResponse<Compensation?>.CreateSuccess(compensation));
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

            return Ok(DynamicResponse<string>.CreateSuccess("Compensación canjeada correctamente"));
        }
        catch (Exception ex)
        {
            return BadRequest(DynamicResponse<string>.CreateError(ex.Message));
        }
    }
}
