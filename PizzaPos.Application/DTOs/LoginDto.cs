namespace PizzaPos.Application.DTOs;

public record LoginRequest(string Username, string Password);

public record LoginResponse(string Token, string Username, string FullName, List<string> Roles, List<string> Permissions);
