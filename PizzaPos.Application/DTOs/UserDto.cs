namespace PizzaPos.Application.DTOs;

public record CreateUserRequest(
    string Username, 
    string Password, 
    List<string> RoleNames, 
    List<string> PermissionNames);

public record UpdateUserStatusRequest(int UserId, bool IsActive);

public record CreateRoleRequest(string Name, List<string> PermissionNames);


public record CreatePermissionRequest(string Name, string Description);

public record UserResponseDto(
    int Id,
    string Username,
    bool IsActive,
    List<string> Roles);
