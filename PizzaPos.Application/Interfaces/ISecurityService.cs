using PizzaPos.Application.DTOs;

namespace PizzaPos.Application.Interfaces;

public interface ISecurityService
{
    Task CreateRoleAsync(CreateRoleRequest request);
    Task CreatePermissionAsync(CreatePermissionRequest request);
}
