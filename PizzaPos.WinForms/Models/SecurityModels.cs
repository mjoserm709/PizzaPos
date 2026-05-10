namespace PizzaPos.WinForms.Models;

public record PermissionResponse(
    int Id,
    string Name,
    string Description,
    bool IsActive,
    DateTime CreatedAt,
    string CreatedBy,
    DateTime? UpdatedAt,
    string? UpdatedBy);

public record RoleResponse(
    int Id,
    string Name,
    bool IsActive,
    List<string> Permissions,
    List<int> PermissionIds,
    DateTime CreatedAt,
    string CreatedBy,
    DateTime? UpdatedAt,
    string? UpdatedBy);

public record CreatePermissionRequest(string Name, string Description);
public record UpdatePermissionRequest(int Id, string Name, string Description);
public record UpdatePermissionStatusRequest(int PermissionId, bool IsActive);

public record CreateRoleRequest(string Name, List<int> PermissionIds);
public record UpdateRoleRequest(int Id, string Name, List<int> PermissionIds);
public record UpdateRoleStatusRequest(int RoleId, bool IsActive);
