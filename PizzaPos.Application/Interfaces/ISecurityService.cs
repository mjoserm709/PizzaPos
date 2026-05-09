using PizzaPos.Application.DTOs;

namespace PizzaPos.Application.Interfaces;

public interface ISecurityService
{
    Task CreateRoleAsync(CreateRoleRequest request, string currentUsername);
    Task UpdateRoleStatusAsync(UpdateRoleStatusRequest request, string currentUsername);
    Task CreatePermissionAsync(CreatePermissionRequest request, string currentUsername);
}
