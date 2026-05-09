using Microsoft.EntityFrameworkCore;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using PizzaPos.Infrastructure.Data;

namespace PizzaPos.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly PizzaPosDbContext _context;

    public PermissionRepository(PizzaPosDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _context.Permissions.ToListAsync();
    }

    public async Task<Permission?> GetByNameAsync(string name)
    {
        return await _context.Permissions.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task AddAsync(Permission permission)
    {
        await _context.Permissions.AddAsync(permission);
        await _context.SaveChangesAsync();
    }
}
