using PizzaPos.Application.DTOs;

namespace PizzaPos.Application.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(CreateUserRequest request, string currentUsername);
    Task UpdateUserAsync(UpdateUserRequest request, string currentUsername);
    Task UpdateStatusAsync(UpdateUserStatusRequest request);
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    Task<IEnumerable<string>> GetRolesAsync();
    Task<IEnumerable<string>> GetPermissionsAsync();
}
