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

    public async Task CreateUserAsync(CreateUserRequest request, string currentUsername)
    {
        if (request.IdentityNumber.Length != 15)
            throw new ArgumentException("La identidad debe tener exactamente 15 caracteres (formato: XXXX-XXXX-XXXXX).");

        // En el nuevo esquema, Username es el Email
        var existingUser = await _userRepository.GetByEmailAsync(request.Username);
        if (existingUser != null)
            throw new ArgumentException("El correo electrónico ya está registrado.");

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            IdentityNumber = request.IdentityNumber,
            CreatedAt = DateTime.Now,
            CreatedBy = currentUsername,
            IsActive = true
        };

        // Tomar el primer rol de la lista (en el nuevo esquema es uno solo)
        if (request.RoleNames.Any())
        {
            var role = await _roleRepository.GetByNameAsync(request.RoleNames.First());
            if (role != null) user.RoleId = role.Id;
        }

        foreach (var permName in request.PermissionNames)
        {
            var perm = await _permissionRepository.GetByNameAsync(permName);
            if (perm != null) user.AdditionalPermissions.Add(perm);
        }

        await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(UpdateUserRequest request, string currentUsername)
    {
        if (request.IdentityNumber.Length != 15)
            throw new ArgumentException("La identidad debe tener exactamente 15 caracteres (formato: XXXX-XXXX-XXXXX).");

        var existingUser = await _userRepository.GetByEmailAsync(request.Username);
        if (existingUser != null && existingUser.Id != request.Id)
            throw new ArgumentException("El correo electrónico ya está siendo usado por otro usuario.");

        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null) throw new KeyNotFoundException("Usuario no encontrado.");

        user.FullName = request.FullName;
        user.Email = request.Username;
        user.IdentityNumber = request.IdentityNumber;
        user.UpdatedAt = DateTime.Now;
        user.UpdatedBy = currentUsername;
        
        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        if (request.RoleNames.Any())
        {
            var role = await _roleRepository.GetByNameAsync(request.RoleNames.First());
            if (role != null) user.RoleId = role.Id;
        }

        user.AdditionalPermissions.Clear();
        foreach (var permName in request.PermissionNames)
        {
            var perm = await _permissionRepository.GetByNameAsync(permName);
            if (perm != null) user.AdditionalPermissions.Add(perm);
        }

        await _userRepository.UpdateAsync(user);
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserResponseDto(
            u.Id,
            u.Email, // Usamos Email como Username para el DTO
            u.FullName,
            u.IdentityNumber,
            u.IsActive,
            u.CreatedAt,
            u.CreatedBy,
            u.UpdatedAt,
            u.UpdatedBy,
            new List<string> { u.Role.Name }, // Adaptamos el rol único a la lista del DTO
            u.AdditionalPermissions.Select(p => p.Name).ToList()
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
        return roles.Where(r => r.IsActive).Select(r => r.Name);
    }

    public async Task<IEnumerable<string>> GetPermissionsAsync()
    {
        var perms = await _permissionRepository.GetAllAsync();
        return perms.Where(p => p.IsActive).Select(p => p.Name);
    }
}
