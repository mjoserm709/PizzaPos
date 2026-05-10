using PizzaPos.Application.DTOs;

namespace PizzaPos.Application.Interfaces;

public interface ISecurityService
{
    Task CreateRoleAsync(CreateRoleRequest request, string currentUsername);
    Task UpdateRoleAsync(UpdateRoleRequest request, string currentUsername);
    Task UpdateRoleStatusAsync(UpdateRoleStatusRequest request, string currentUsername);
    Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync();

    Task CreatePermissionAsync(CreatePermissionRequest request, string currentUsername);
    Task UpdatePermissionAsync(UpdatePermissionRequest request, string currentUsername);
    Task UpdatePermissionStatusAsync(UpdatePermissionStatusRequest request, string currentUsername);
    Task<IEnumerable<PermissionResponseDto>> GetAllPermissionsAsync();
}
