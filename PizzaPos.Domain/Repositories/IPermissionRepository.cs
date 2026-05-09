using PizzaPos.Domain.Entities;

namespace PizzaPos.Domain.Repositories;

public interface IPermissionRepository
{
    Task<IEnumerable<Permission>> GetAllAsync();
    Task<Permission?> GetByNameAsync(string name);
    Task<Permission?> GetByIdAsync(int id);
    Task AddAsync(Permission permission);
    Task UpdateAsync(Permission permission);
}
