using PizzaPos.Application.DTOs;

namespace PizzaPos.Application.Interfaces;

public interface ILoginService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}
