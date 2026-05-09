using PizzaPos.Application.DTOs;
using PizzaPos.Application.Interfaces;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;

namespace PizzaPos.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;

    public UserService(
        IUserRepository userRepository, 
        IRoleRepository roleRepository, 
        IPermissionRepository permissionRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task CreateUserAsync(CreateUserRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            PasswordHash = request.Password,
            IsActive = true
        };

        foreach (var roleName in request.RoleNames)
        {
            var role = await _roleRepository.GetByNameAsync(roleName);
            if (role != null) user.Roles.Add(role);
        }

        foreach (var permName in request.PermissionNames)
        {
            var perm = await _permissionRepository.GetByNameAsync(permName);
            if (perm != null) user.AdditionalPermissions.Add(perm);
        }

        await _userRepository.AddAsync(user);
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserResponseDto(
            u.Id,
            u.Username,
            u.IsActive,
            u.Roles.Select(r => r.Name).ToList()
        ));
    }

    public async Task UpdateStatusAsync(UpdateUserStatusRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user != null)
        {
            user.IsActive = request.IsActive;
            await _userRepository.UpdateAsync(user);
        }
    }

    public async Task<IEnumerable<string>> GetRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(r => r.Name);
    }

    public async Task<IEnumerable<string>> GetPermissionsAsync()
    {
        var perms = await _permissionRepository.GetAllAsync();
        return perms.Select(p => p.Name);
    }
}
