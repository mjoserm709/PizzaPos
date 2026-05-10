using Microsoft.EntityFrameworkCore;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using PizzaPos.Infrastructure.Data;

namespace PizzaPos.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly PizzaPosDbContext _context;
    private readonly DbSet<Product> _dbSet;

    public ProductRepository(PizzaPosDbContext context)
    {
        _context = context;
        _dbSet = context.Products;
    }

    public async Task<Product?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<Product>> GetAllAsync() => 
        await _dbSet.Include(p => p.Category).ToListAsync();

    public async Task AddAsync(Product product)
    {
        await _dbSet.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _dbSet.Update(product);
        await _context.SaveChangesAsync();
    }
}

public class OrderStatusRepository : IOrderStatusRepository
{
    private readonly DbSet<OrderStatus> _dbSet;

    public OrderStatusRepository(PizzaPosDbContext context)
    {
        _dbSet = context.OrderStatuses;
    }

    public async Task<OrderStatus?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<OrderStatus?> GetByCodeAsync(string code)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.Code == code);
    }
}
