using PizzaPos.Application.DTOs;

namespace PizzaPos.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(CreateOrderRequest request, string currentUsername);
    Task<IEnumerable<OrderResponseDto>> GetOrdersByStatusAsync(string statusCode);
    Task<IEnumerable<OrderResponseDto>> GetActiveOrdersAsync();
    Task<IEnumerable<OrderResponseDto>> GetOrderHistoryAsync(DateTime? startDate, DateTime? endDate, string? searchTerm);
    Task<OrderResponseDto?> GetOrderByIdAsync(int id);
    Task<decimal> GetIvaRateAsync();
    Task UpdateOrderStatusAsync(int orderId, string statusCode, string currentUsername);
}

// DTOs Necesarios
public record CreateOrderRequest(
    int CustomerId,
    int? AddressId,
    int PaymentMethodId,
    List<OrderItemRequest> Items,
    string? Notes
);

public record OrderItemRequest(int ProductId, int Quantity, decimal UnitPrice);

public record OrderResponseDto(
    int Id,
    string OrderNumber,
    string CustomerName,
    string StatusName,
    string StatusCode,
    int StatusId,
    decimal Subtotal,
    decimal TaxAmount,
    decimal Total,
    DateTime CreatedAt,
    List<OrderDetailDto> Details
);

public record OrderDetailDto(string ProductName, int Quantity, decimal UnitPrice, decimal Total);
