using PizzaPos.Domain.Entities;

namespace PizzaPos.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
}

public interface IOrderStatusRepository
{
    Task<OrderStatus?> GetByIdAsync(int id);
    Task<OrderStatus?> GetByCodeAsync(string code);
}
