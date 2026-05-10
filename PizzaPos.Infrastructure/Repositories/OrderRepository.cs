using Microsoft.EntityFrameworkCore;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using PizzaPos.Infrastructure.Data;

namespace PizzaPos.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly PizzaPosDbContext _context;
    private readonly DbSet<Customer> _dbSet;

    public CustomerRepository(PizzaPosDbContext context)
    {
        _context = context;
        _dbSet = context.Customers;
    }

    public async Task<Customer?> GetByIdAsync(int id) => await _dbSet.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Customer?> GetByPhoneAsync(string phone)
    {
        return await _dbSet.Include(c => c.Addresses)
                           .FirstOrDefaultAsync(c => c.Phone == phone);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _dbSet.Include(c => c.Addresses).ToListAsync();
    }

    public async Task<IEnumerable<Customer>> SearchAsync(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return await GetAllAsync();
        }
        
        return await _dbSet.Include(c => c.Addresses)
                           .Where(c => c.FullName.Contains(term) || c.Phone.Contains(term) || c.Email.Contains(term))
                           .ToListAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        await _dbSet.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _dbSet.Update(customer);
        await _context.SaveChangesAsync();
    }
}

public class OrderRepository : IOrderRepository
{
    private readonly PizzaPosDbContext _context;
    private readonly DbSet<Order> _dbSet;

    public OrderRepository(PizzaPosDbContext context)
    {
        _context = context;
        _dbSet = context.Orders;
    }

    public async Task<Order?> GetByIdAsync(int id) => await _dbSet.Include(o => o.Details).FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<Order>> GetByStatusAsync(string statusCode)
    {
        return await _dbSet.Include(o => o.Customer)
                           .Include(o => o.Status)
                           .Include(o => o.Details)
                           .Where(o => o.Status.Code == statusCode)
                           .OrderByDescending(o => o.CreatedAt)
                           .ToListAsync();
    }

    public async Task<string> GetNextOrderNumberAsync()
    {
        var count = await _dbSet.CountAsync();
        return $"ORD-{(count + 1):D4}";
    }

    public async Task AddAsync(Order order)
    {
        await _dbSet.AddAsync(order);
        await _context.SaveChangesAsync();
    }
}
