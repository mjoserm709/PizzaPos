namespace PizzaPos.WinForms.Models;

public class CustomerModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public List<AddressModel> Addresses { get; set; } = new();
}

public class AddressModel
{
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string? Sector { get; set; }
    public string? City { get; set; }
    public string? Reference { get; set; }
    public bool IsPrimary { get; set; }
}

public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    public ProductCategoryModel? Category { get; set; }
}

public class ProductCategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
