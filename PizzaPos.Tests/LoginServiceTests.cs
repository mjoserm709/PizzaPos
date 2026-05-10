using Moq;
using PizzaPos.Application.Services;
using PizzaPos.Domain.Repositories;
using PizzaPos.Domain.Entities;
using PizzaPos.Application.DTOs;
using PizzaPos.Application.Interfaces;
using BCrypt.Net;
using Xunit;

namespace PizzaPos.Tests;

public class LoginServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IJwtTokenGenerator> _tokenServiceMock;
    private readonly LoginService _loginService;

    public LoginServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _tokenServiceMock = new Mock<IJwtTokenGenerator>();
        _loginService = new LoginService(_userRepoMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ReturnsToken_WhenPasswordIsCorrectBCrypt()
    {
        // Arrange
        string password = "secretPassword";
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        
        var user = new User
        {
            Email = "test@pizzeria.com",
            PasswordHash = hashedPassword,
            IsActive = true,
            Role = new Role { Name = "admin" }
        };

        _userRepoMock.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _tokenServiceMock.Setup(t => t.GenerateToken(user)).Returns("mock-token");

        // Act
        var result = await _loginService.LoginAsync(new LoginRequest(user.Email, password));

        // Assert
        Assert.NotNull(result);
        Assert.Equal("mock-token", result.Token);
    }

    [Fact]
    public async Task LoginAsync_ReturnsToken_WhenPasswordIsPlainText_FallbackWorks()
    {
        // Arrange
        string password = "plainTextPassword";
        
        var user = new User
        {
            Email = "plain@pizzeria.com",
            PasswordHash = password,
            IsActive = true,
            Role = new Role { Name = "admin" }
        };

        _userRepoMock.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _tokenServiceMock.Setup(t => t.GenerateToken(user)).Returns("fallback-token");

        // Act
        var result = await _loginService.LoginAsync(new LoginRequest(user.Email, password));

        // Assert
        Assert.NotNull(result);
        Assert.Equal("fallback-token", result.Token);
    }

    [Fact]
    public async Task LoginAsync_ReturnsNull_WhenPasswordIsIncorrect()
    {
        // Arrange
        var user = new User
        {
            Email = "test@pizzeria.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("correctPassword"),
            IsActive = true
        };

        _userRepoMock.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);

        // Act
        var result = await _loginService.LoginAsync(new LoginRequest(user.Email, "wrongPassword"));

        // Assert
        Assert.Null(result);
    }
}
