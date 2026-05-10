using Microsoft.AspNetCore.SignalR;

namespace PizzaPos.Api.Hubs;

public class OrderHub : Hub
{
    public async Task SendOrderUpdate(string message)
    {
        await Clients.All.SendAsync("ReceiveOrderUpdate", message);
    }

    public async Task NotifyNewOrder(object order)
    {
        await Clients.All.SendAsync("NewOrderReceived", order);
    }

    public async Task NotifyStatusChanged(int orderId, string newStatus)
    {
        await Clients.All.SendAsync("OrderStatusChanged", new { OrderId = orderId, Status = newStatus });
    }
}
