using Microsoft.Extensions.Configuration;

namespace PizzaPos.Infrastructure.Examples;

/// <summary>
/// Ejemplo de una capa de abstracción para secretos.
/// Esta clase demuestra cómo centralizar el acceso a datos sensibles (JWT, Connection Strings)
/// evitando el uso de strings "mágicos" en todo el código.
/// </summary>
public class SecretManagementExample
{
    private readonly IConfiguration _configuration;

    public SecretManagementExample(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Ejemplo: Obtener la llave de JWT desde User Secrets o Variables de Entorno
    public string GetJwtSecret()
    {
        // En producción, esto vendría de un Key Vault o Variables de Entorno seguras
        return _configuration["Jwt:Key"] ?? "DefaultVerySecureKey123!";
    }

    // Ejemplo: Obtener credenciales de base de datos
    public string GetDbConnectionString()
    {
        return _configuration.GetConnectionString("DefaultConnection") ?? "";
    }

    /* 
       RECOMENDACIÓN DE SEGURIDAD:
       Para desarrollo local, usa: dotnet user-secrets set "Jwt:Key" "TuLlaveSuperSecreta"
       Para producción, usa: Variables de entorno del servidor o Azure Key Vault.
    */
}
