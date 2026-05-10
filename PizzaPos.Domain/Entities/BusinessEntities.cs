using System.ComponentModel.DataAnnotations;

namespace PizzaPos.Domain.Entities;

public class Customer : BaseEntity
{
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    
    [EmailAddress]
    public string? Email { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}

public class Address : BaseEntity
{
    public int CustomerId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string? Number { get; set; }
    public string? Sector { get; set; }
    public string? City { get; set; }
    public string? Reference { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsActive { get; set; } = true;
    
    public Customer Customer { get; set; } = null!;
}

public class Product : BaseEntity
{
    public int CategoryId { get; set; }
    public int? SizeId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
    
    public ProductCategory Category { get; set; } = null!;
    public ProductSize? Size { get; set; }
}

public class Order : BaseEntity
{
    [Required]
    public string OrderNumber { get; set; } = string.Empty;
    
    public int CustomerId { get; set; }
    public int? AddressId { get; set; }
    public int StatusId { get; set; }
    public int PaymentMethodId { get; set; }
    
    public decimal Subtotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal Total { get; set; }
    
    public string? Notes { get; set; }

    public Customer Customer { get; set; } = null!;
    public Address? Address { get; set; }
    public OrderStatus Status { get; set; } = null!;
    public PaymentMethod PaymentMethod { get; set; } = null!;
    public ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();
}

public class OrderDetail : BaseEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }

    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}

public class Delivery : BaseEntity
{
    public int OrderId { get; set; }
    public int? CourierId { get; set; }
    public int DeliveryStatusId { get; set; }
    public DateTime? AssignedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public string? Notes { get; set; }

    public Order Order { get; set; } = null!;
    public User? Courier { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; } = null!;
}
