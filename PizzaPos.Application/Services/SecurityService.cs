using PizzaPos.Application.DTOs;
using PizzaPos.Application.Interfaces;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;

namespace PizzaPos.Application.Services;

public class SecurityService : ISecurityService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;

    public SecurityService(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task CreateRoleAsync(CreateRoleRequest request, string currentUsername)
    {
        var role = new Role 
        { 
            Name = request.Name,
            CreatedAt = DateTime.Now,
            CreatedBy = currentUsername,
            IsActive = true
        };
        
        foreach (var permId in request.PermissionIds)
        {
            var perm = await _permissionRepository.GetByIdAsync(permId);
            if (perm != null) role.Permissions.Add(perm);
        }

        await _roleRepository.AddAsync(role);
    }

    public async Task UpdateRoleAsync(UpdateRoleRequest request, string currentUsername)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);
        if (role == null) throw new Exception("Rol no encontrado");

        role.Name = request.Name;
        role.UpdatedAt = DateTime.Now;
        role.UpdatedBy = currentUsername;

        role.Permissions.Clear();
        foreach (var permId in request.PermissionIds)
        {
            var perm = await _permissionRepository.GetByIdAsync(permId);
            if (perm != null) role.Permissions.Add(perm);
        }

        await _roleRepository.UpdateAsync(role);
    }

    public async Task UpdateRoleStatusAsync(UpdateRoleStatusRequest request, string currentUsername)
    {
        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        if (role != null)
        {
            role.IsActive = request.IsActive;
            role.UpdatedAt = DateTime.Now;
            role.UpdatedBy = currentUsername;
            await _roleRepository.UpdateAsync(role);
        }
    }

    public async Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(r => new RoleResponseDto(
            r.Id,
            r.Name,
            r.IsActive,
            r.Permissions.Select(p => p.Name).ToList(),
            r.Permissions.Select(p => p.Id).ToList(),
            r.CreatedAt,
            r.CreatedBy,
            r.UpdatedAt,
            r.UpdatedBy
        ));
    }

    public async Task CreatePermissionAsync(CreatePermissionRequest request, string currentUsername)
    {
        var permission = new Permission 
        { 
            Name = request.Name, 
            Description = request.Description,
            CreatedAt = DateTime.Now,
            CreatedBy = currentUsername,
            IsActive = true
        };
        await _permissionRepository.AddAsync(permission);
    }

    public async Task UpdatePermissionAsync(UpdatePermissionRequest request, string currentUsername)
    {
        var permission = await _permissionRepository.GetByIdAsync(request.Id);
        if (permission == null) throw new Exception("Permiso no encontrado");

        permission.Name = request.Name;
        permission.Description = request.Description;
        permission.UpdatedAt = DateTime.Now;
        permission.UpdatedBy = currentUsername;

        await _permissionRepository.UpdateAsync(permission);
    }

    public async Task UpdatePermissionStatusAsync(UpdatePermissionStatusRequest request, string currentUsername)
    {
        var permission = await _permissionRepository.GetByIdAsync(request.PermissionId);
        if (permission != null)
        {
            permission.IsActive = request.IsActive;
            permission.UpdatedAt = DateTime.Now;
            permission.UpdatedBy = currentUsername;
            await _permissionRepository.UpdateAsync(permission);
        }
    }

    public async Task<IEnumerable<PermissionResponseDto>> GetAllPermissionsAsync()
    {
        var perms = await _permissionRepository.GetAllAsync();
        return perms.Select(p => new PermissionResponseDto(
            p.Id,
            p.Name,
            p.Description,
            p.IsActive,
            p.CreatedAt,
            p.CreatedBy,
            p.UpdatedAt,
            p.UpdatedBy
        ));
    }
}
