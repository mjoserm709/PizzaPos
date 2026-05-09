using Microsoft.EntityFrameworkCore;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using PizzaPos.Infrastructure.Data;

namespace PizzaPos.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PizzaPosDbContext _context;

    public UserRepository(PizzaPosDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
            .Include(u => u.AdditionalPermissions)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Roles)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Roles)
            .Include(u => u.AdditionalPermissions)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
