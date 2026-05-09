using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PizzaPos.Application.Interfaces;
using PizzaPos.Domain.Entities;

namespace PizzaPos.Infrastructure.Identity;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var secretKey = _configuration["Jwt:Key"] ?? "super_secret_key_that_is_at_least_32_characters_long";
        var issuer = _configuration["Jwt:Issuer"] ?? "PizzaPosApi";
        var audience = _configuration["Jwt:Audience"] ?? "PizzaPosWinForms";

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        // Add multiple roles
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
            
            // Add permissions from roles
            foreach (var permission in role.Permissions)
            {
                claims.Add(new Claim("permission", permission.Name));
            }
        }

        // Add additional individual permissions
        foreach (var permission in user.AdditionalPermissions)
        {
            claims.Add(new Claim("permission", permission.Name));
        }

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
