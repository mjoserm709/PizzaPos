using PizzaPos.Application.DTOs;
using PizzaPos.Application.Interfaces;
using PizzaPos.Domain.Repositories;

namespace PizzaPos.Application.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        // En tu esquema, el login se hace por Email
        var user = await _userRepository.GetByEmailAsync(request.Username);

        if (user == null || user.PasswordHash != request.Password || !user.IsActive)
        {
            return null;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        
        // Un solo rol ahora
        var roles = new List<string> { user.Role.Name };
        
        // Permisos del rol + permisos adicionales
        var permissions = user.Role.Permissions.Select(p => p.Name)
            .Concat(user.AdditionalPermissions.Select(p => p.Name))
            .Distinct()
            .ToList();

        return new LoginResponse(token, user.Email, user.FullName, roles, permissions);
    }
}
