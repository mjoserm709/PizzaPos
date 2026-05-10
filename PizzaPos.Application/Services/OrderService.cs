using PizzaPos.Application.Interfaces;
using PizzaPos.Domain.Entities;
using PizzaPos.Domain.Repositories;
using PizzaPos.Application.DTOs;

namespace PizzaPos.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAppConfigRepository _configRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderStatusRepository _statusRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IAppConfigRepository configRepository,
        IProductRepository productRepository,
        IOrderStatusRepository statusRepository)
    {
        _orderRepository = orderRepository;
        _configRepository = configRepository;
        _productRepository = productRepository;
        _statusRepository = statusRepository;
    }

    public async Task<decimal> GetIvaRateAsync()
    {
        var value = await _configRepository.GetValueAsync("IVA_PERCENTAGE");
        if (decimal.TryParse(value, out decimal rate))
        {
            return rate / 100m;
        }
        return 0.13m;
    }

    public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderRequest request, string currentUsername)
    {
        var ivaRate = await GetIvaRateAsync();
        var orderNumber = await _orderRepository.GetNextOrderNumberAsync();
        
        var initialStatus = await _statusRepository.GetByIdAsync(1);

        decimal subtotal = 0;
        var details = new List<OrderDetail>();

        foreach (var item in request.Items)
        {
            var totalItem = item.Quantity * item.UnitPrice;
            subtotal += totalItem;

            details.Add(new OrderDetail
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Total = totalItem,
                CreatedAt = DateTime.Now,
                CreatedBy = currentUsername
            });
        }

        var taxAmount = subtotal * ivaRate;
        var total = subtotal + taxAmount;

        var order = new Order
        {
            OrderNumber = orderNumber,
            CustomerId = request.CustomerId,
            AddressId = request.AddressId,
            StatusId = initialStatus?.Id ?? 1,
            PaymentMethodId = request.PaymentMethodId,
            Subtotal = subtotal,
            TaxAmount = taxAmount,
            Total = total,
            Notes = request.Notes,
            CreatedAt = DateTime.Now,
            CreatedBy = currentUsername,
            Details = details
        };

        await _orderRepository.AddAsync(order);

        return MapToDto(order);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetOrdersByStatusAsync(string statusCode)
    {
        var orders = await _orderRepository.GetByStatusAsync(statusCode);
        return orders.Select(MapToDto);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetActiveOrdersAsync()
    {
        var orders = await _orderRepository.GetActiveOrdersAsync();
        return orders.Select(MapToDto);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetOrderHistoryAsync(DateTime? startDate, DateTime? endDate, string? searchTerm)
    {
        var orders = await _orderRepository.GetHistoryAsync(startDate, endDate, searchTerm);
        return orders.Select(MapToDto);
    }

    public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return order != null ? MapToDto(order) : null;
    }

    public async Task UpdateOrderStatusAsync(int orderId, string statusCode, string currentUsername)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null) throw new Exception("Pedido no encontrado");

        var status = await _statusRepository.GetByCodeAsync(statusCode);
        if (status == null) throw new Exception("Estado no válido");

        order.StatusId = status.Id;
        order.UpdatedAt = DateTime.Now;
        order.UpdatedBy = currentUsername;

        await _orderRepository.UpdateAsync(order);
    }

    private OrderResponseDto MapToDto(Order o)
    {
        return new OrderResponseDto(
            o.Id,
            o.OrderNumber,
            o.Customer?.FullName ?? "Cliente",
            o.Customer?.Phone ?? "Sin teléfono",
            o.Address != null ? $"{o.Address.Street} {o.Address.Number}, {o.Address.Sector}" : "Retiro en local",
            o.Notes ?? "",
            o.Status?.Name ?? "Pendiente",
            o.Status?.Code ?? "pendiente",
            o.StatusId,
            o.Subtotal,
            o.TaxAmount,
            o.Total,
            o.CreatedAt,
            o.Details.Select(d => new OrderDetailDto(
                d.Product?.Name ?? "Producto",
                d.Quantity,
                d.UnitPrice,
                d.Total
            )).ToList()
        );
    }
}
