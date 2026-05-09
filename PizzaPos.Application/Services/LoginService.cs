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
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user == null || user.PasswordHash != request.Password || !user.IsActive)
        {
            return null;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        
        var roles = user.Roles.Select(r => r.Name).ToList();
        var permissions = user.Roles.SelectMany(r => r.Permissions.Select(p => p.Name))
            .Concat(user.AdditionalPermissions.Select(p => p.Name))
            .Distinct()
            .ToList();

        return new LoginResponse(token, user.Username, roles, permissions);
    }
}
