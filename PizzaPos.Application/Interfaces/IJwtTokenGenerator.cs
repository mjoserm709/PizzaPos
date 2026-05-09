using PizzaPos.Domain.Entities;

namespace PizzaPos.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
