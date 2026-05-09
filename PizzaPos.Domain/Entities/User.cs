namespace PizzaPos.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string IdentityNumber { get; set; } = string.Empty;

    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<Permission> AdditionalPermissions { get; set; } = new List<Permission>();
}
