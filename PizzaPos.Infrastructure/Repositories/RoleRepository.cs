using Microsoft.EntityFrameworkCore;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using PizzaPos.Infrastructure.Data;

namespace PizzaPos.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly PizzaPosDbContext _context;

    public RoleRepository(PizzaPosDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles
            .Include(r => r.Permissions)
            .ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
    }
}
