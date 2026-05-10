using PizzaPos.Domain.Entities;

namespace PizzaPos.Domain.Repositories;

public interface ICompensationRepository
{
    Task<Compensation?> GetPendingByCustomerIdAsync(int customerId);
    Task AddAsync(Compensation compensation);
    Task UpdateAsync(Compensation compensation);
}
