using Microsoft.EntityFrameworkCore;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using PizzaPos.Infrastructure.Data;

namespace PizzaPos.Infrastructure.Repositories;

public class CompensationRepository : ICompensationRepository
{
    private readonly PizzaPosDbContext _context;
    private readonly DbSet<Compensation> _dbSet;

    public CompensationRepository(PizzaPosDbContext context)
    {
        _context = context;
        _dbSet = context.Compensations;
    }

    public async Task<Compensation?> GetPendingByCustomerIdAsync(int customerId)
    {
        return await _dbSet
            .Where(c => c.CustomerId == customerId && !c.IsRedeemed)
            .OrderByDescending(c => c.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Compensation compensation)
    {
        await _dbSet.AddAsync(compensation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Compensation compensation)
    {
        _dbSet.Update(compensation);
        await _context.SaveChangesAsync();
    }
}
