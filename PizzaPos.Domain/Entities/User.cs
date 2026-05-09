namespace PizzaPos.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<Permission> AdditionalPermissions { get; set; } = new List<Permission>();
}
