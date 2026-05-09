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

    public async Task CreateRoleAsync(CreateRoleRequest request)
    {
        var role = new Role { Name = request.Name };
        
        foreach (var permName in request.PermissionNames)
        {
            var perm = await _permissionRepository.GetByNameAsync(permName);
            if (perm != null) role.Permissions.Add(perm);
        }

        await _roleRepository.AddAsync(role);
    }

    public async Task CreatePermissionAsync(CreatePermissionRequest request)
    {
        var permission = new Permission 
        { 
            Name = request.Name, 
            Description = request.Description 
        };
        await _permissionRepository.AddAsync(permission);
    }
}
