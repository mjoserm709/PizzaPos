using Microsoft.EntityFrameworkCore;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using PizzaPos.Infrastructure.Data;

namespace PizzaPos.Infrastructure.Repositories;

public class AppConfigRepository : IAppConfigRepository
{
    private readonly DbSet<AppConfig> _dbSet;

    public AppConfigRepository(PizzaPosDbContext context)
    {
        _dbSet = context.Configs;
    }

    public async Task<string?> GetValueAsync(string key)
    {
        var config = await _dbSet.FirstOrDefaultAsync(c => c.Key == key);
        return config?.Value;
    }
}
