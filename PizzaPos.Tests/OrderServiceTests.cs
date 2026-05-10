using Moq;
using PizzaPos.Application.Services;
using PizzaPos.Domain.Repositories;
using PizzaPos.Domain.Entities;
using PizzaPos.Application.Interfaces;
using Xunit;

namespace PizzaPos.Tests;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<IAppConfigRepository> _configRepoMock;
    private readonly Mock<IProductRepository> _productRepoMock;
    private readonly Mock<IOrderStatusRepository> _statusRepoMock;
    private readonly Mock<ICompensationRepository> _compensationRepoMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderRepoMock = new Mock<IOrderRepository>();
        _configRepoMock = new Mock<IAppConfigRepository>();
        _productRepoMock = new Mock<IProductRepository>();
        _statusRepoMock = new Mock<IOrderStatusRepository>();
        _compensationRepoMock = new Mock<ICompensationRepository>();

        _orderService = new OrderService(
            _orderRepoMock.Object,
            _configRepoMock.Object,
            _productRepoMock.Object,
            _statusRepoMock.Object,
            _compensationRepoMock.Object
        );
    }

    [Fact]
    public async Task CreateOrderAsync_CalculatesCorrectTotal()
    {
        // Arrange
        _configRepoMock.Setup(c => c.GetValueAsync("IVA_PERCENTAGE")).ReturnsAsync("15");
        _orderRepoMock.Setup(o => o.GetNextOrderNumberAsync()).ReturnsAsync("ORD-001");
        
        var request = new CreateOrderRequest(
            CustomerId: 1,
            AddressId: 1,
            PaymentMethodId: 1,
            Items: new List<OrderItemRequest>
            {
                new OrderItemRequest(1, 2, 100) // 2 * 100 = 200
            },
            Notes: "Test order"
        );

        // Act
        var result = await _orderService.CreateOrderAsync(request, "admin");

        // Assert
        Assert.Equal(200, result.Subtotal);
        Assert.Equal(30, result.TaxAmount);
        Assert.Equal(230, result.Total);
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_CreatesCompensation_WhenDelayedMoreThan60Minutes()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            OrderNumber = "ORD-LATE",
            CustomerId = 1,
            CreatedAt = DateTime.Now.AddMinutes(-70),
            StatusId = 4
        };

        _orderRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(order);
        _statusRepoMock.Setup(r => r.GetByCodeAsync("entregado")).ReturnsAsync(new OrderStatus { Id = 6, Code = "entregado" });
        _compensationRepoMock.Setup(r => r.GetPendingByCustomerIdAsync(1)).ReturnsAsync((Compensation?)null);

        // Act
        await _orderService.UpdateOrderStatusAsync(1, "entregado", "admin");

        // Assert
        _compensationRepoMock.Verify(r => r.AddAsync(It.Is<Compensation>(c => 
            c.CustomerId == 1 && 
            c.DiscountAmount == 0.20m)), Times.Once);
    }
}
