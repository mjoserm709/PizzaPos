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
    public int? SizeId { get; set; }
    public ProductSizeModel? Size { get; set; }
}

public class ProductCategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class ProductSizeModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class OrderResponseDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public string StatusCode { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<OrderDetailDto> Details { get; set; } = new();
}

public class OrderDetailDto
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}
