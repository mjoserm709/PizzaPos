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

        if (user == null || !user.IsActive)
        {
            return null;
        }

        bool isValid = false;
        try
        {
            isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        }
        catch
        {
            // Si el hash en la DB no es un hash válido de BCrypt (ej. texto plano antiguo),
            // comparamos directamente. Esto evita bloqueos durante la transición.
            isValid = user.PasswordHash == request.Password;
        }

        if (!isValid)
        {
            return null;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        
        // Un solo rol ahora
        var roleName = user.Role?.Name ?? "User";
        var roles = new List<string> { roleName };
        
        // Permisos del rol + permisos adicionales
        var rolePermissions = user.Role?.Permissions.Select(p => p.Name) ?? Enumerable.Empty<string>();
        var additionalPermissions = user.AdditionalPermissions?.Select(p => p.Name) ?? Enumerable.Empty<string>();

        var permissions = rolePermissions
            .Concat(additionalPermissions)
            .Distinct()
            .ToList();

        return new LoginResponse(token, user.Email, user.FullName, roles, permissions);
    }
}
