using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using System.Security.Claims;

namespace PizzaPos.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _productRepository.GetAllAsync();
        return Ok(new { success = true, data = results });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _productRepository.GetByIdAsync(id);
        if (result == null) return NotFound(new { success = false, message = "Producto no encontrado" });
        return Ok(new { success = true, data = result });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        var currentEmail = User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
        try
        {
            product.CreatedAt = DateTime.Now;
            product.CreatedBy = currentEmail;
            await _productRepository.AddAsync(product);
            return Ok(new { success = true, message = "Producto creado correctamente", data = product });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        try
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { success = false, message = "Producto no encontrado" });

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            existing.CategoryId = product.CategoryId;
            existing.SizeId = product.SizeId;
            
            await _productRepository.UpdateAsync(existing);
            return Ok(new { success = true, message = "Producto actualizado correctamente" });
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
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { success = false, message = "Producto no encontrado" });

            existing.IsActive = !existing.IsActive;
            await _productRepository.UpdateAsync(existing);
            return Ok(new { success = true, message = $"Producto {(existing.IsActive ? "activado" : "desactivado")} correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var results = await _productRepository.GetCategoriesAsync();
        return Ok(new { success = true, data = results });
    }

    [HttpGet("sizes")]
    public async Task<IActionResult> GetSizes()
    {
        var results = await _productRepository.GetSizesAsync();
        return Ok(new { success = true, data = results });
    }
}
