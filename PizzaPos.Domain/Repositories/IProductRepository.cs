using PizzaPos.Domain.Entities;

namespace PizzaPos.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
}

public interface IOrderStatusRepository
{
    Task<OrderStatus?> GetByIdAsync(int id);
    Task<OrderStatus?> GetByCodeAsync(string code);
}
