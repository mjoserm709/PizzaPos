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
        
        foreach (var permName in request.PermissionNames)
        {
            var perm = await _permissionRepository.GetByNameAsync(permName);
            if (perm != null) role.Permissions.Add(perm);
        }

        await _roleRepository.AddAsync(role);
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
}
