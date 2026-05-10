using System.ComponentModel.DataAnnotations;

namespace PizzaPos.Domain.Entities;

public class AppConfig : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Key { get; set; } = string.Empty;

    [Required]
    public string Value { get; set; } = string.Empty;

    public string? Description { get; set; }
}
