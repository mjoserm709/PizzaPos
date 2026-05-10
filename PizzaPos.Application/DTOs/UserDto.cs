namespace PizzaPos.Application.DTOs;

public record CreateUserRequest(
    string Username, 
    string Password, 
    string FullName,
    string IdentityNumber,
    List<string> RoleNames, 
    List<string> PermissionNames);

public record UpdateUserRequest(
    int Id,
    string Username,
    string? Password, 
    string FullName,
    string IdentityNumber,
    List<string> RoleNames, 
    List<string> PermissionNames);

public record UpdateUserStatusRequest(int UserId, bool IsActive);

public record UpdateRoleStatusRequest(int RoleId, bool IsActive);

public record CreateRoleRequest(string Name, List<int> PermissionIds);

public record CreatePermissionRequest(string Name, string Description);

public record UpdateRoleRequest(int Id, string Name, List<int> PermissionIds);

public record UpdatePermissionRequest(int Id, string Name, string Description);

public record UpdatePermissionStatusRequest(int PermissionId, bool IsActive);

public record RoleResponseDto(
    int Id,
    string Name,
    bool IsActive,
    List<string> Permissions,
    List<int> PermissionIds,
    DateTime CreatedAt,
    string CreatedBy,
    DateTime? UpdatedAt,
    string? UpdatedBy);

public record PermissionResponseDto(
    int Id,
    string Name,
    string Description,
    bool IsActive,
    DateTime CreatedAt,
    string CreatedBy,
    DateTime? UpdatedAt,
    string? UpdatedBy);

public record UserResponseDto(
    int Id,
    string Username,
    string FullName,
    string IdentityNumber,
    bool IsActive,
    DateTime CreatedAt,
    string CreatedBy,
    DateTime? UpdatedAt,
    string? UpdatedBy,
    List<string> Roles,
    List<string> Permissions);
