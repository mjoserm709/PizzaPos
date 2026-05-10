namespace PizzaPos.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public string IdentityNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public Role Role { get; set; } = null!;
    public ICollection<Permission> AdditionalPermissions { get; set; } = new List<Permission>();
}
