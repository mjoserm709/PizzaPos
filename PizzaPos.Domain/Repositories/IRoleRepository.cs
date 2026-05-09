using PizzaPos.Domain.Entities;

namespace PizzaPos.Domain.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string name);
    Task<IEnumerable<Role>> GetAllAsync();
    Task AddAsync(Role role);
}
