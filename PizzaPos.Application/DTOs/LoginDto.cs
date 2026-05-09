namespace PizzaPos.Application.DTOs;

public record LoginRequest(string Username, string Password);

public record LoginResponse(
    int Id,
    string Username,
    string Role,
    List<string> Permissions,
    string Token
);
