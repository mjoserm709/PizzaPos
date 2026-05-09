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

public record CreateRoleRequest(string Name, List<string> PermissionNames);

public record CreatePermissionRequest(string Name, string Description);

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
