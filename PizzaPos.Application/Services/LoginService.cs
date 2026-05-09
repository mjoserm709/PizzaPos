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

        // Basic password check (in production use BCrypt or similar)
        if (user == null || user.PasswordHash != request.Password)
        {
            return null;
        }

        if (!user.IsActive)
        {
            throw new Exception("User is inactive");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new LoginResponse(
            user.Id,
            user.Username,
            user.Role.Name,
            user.Role.Permissions.Select(p => p.Name).ToList(),
            token
        );
    }
}
