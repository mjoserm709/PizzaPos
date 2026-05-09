using PizzaPos.Domain.Entities;

namespace PizzaPos.Domain.Repositories;

public interface IPermissionRepository
{
    Task<IEnumerable<Permission>> GetAllAsync();
    Task<Permission?> GetByNameAsync(string name);
    Task AddAsync(Permission permission);
}
