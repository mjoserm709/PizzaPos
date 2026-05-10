using PizzaPos.Domain.Entities;

namespace PizzaPos.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer?> GetByPhoneAsync(string phone);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<IEnumerable<Customer>> SearchAsync(string term);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
}

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetByStatusAsync(string statusCode);
    Task<IEnumerable<Order>> GetActiveOrdersAsync();
    Task<IEnumerable<Order>> GetHistoryAsync(DateTime? startDate, DateTime? endDate, string? searchTerm);
    Task<string> GetNextOrderNumberAsync();
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}

public interface IAppConfigRepository
{
    Task<string?> GetValueAsync(string key);
}
