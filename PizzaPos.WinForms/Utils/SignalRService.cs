using Microsoft.AspNetCore.SignalR.Client;

namespace PizzaPos.WinForms.Utils;

public class SignalRService
{
    private static SignalRService? _instance;
    private HubConnection? _connection;
    private readonly string _hubUrl = "http://localhost:5267/orderHub";

    public static SignalRService Instance => _instance ??= new SignalRService();

    private SignalRService() { }

    public async Task StartAsync()
    {
        if (_connection != null) return;

        _connection = new HubConnectionBuilder()
            .WithUrl(_hubUrl)
            .WithAutomaticReconnect()
            .Build();

        _connection.On<string>("ReceiveOrderUpdate", (message) =>
        {
            ToastNotification.Info("Notificación: " + message);
        });

        _connection.On<object>("NewOrderReceived", (order) =>
        {
            ToastNotification.Success("¡Nueva Orden Recibida!");
            // Disparar evento local si es necesario
            OnNewOrderReceived?.Invoke(order);
        });

        _connection.On<object>("OrderStatusChanged", (data) =>
        {
            ToastNotification.Info("Estado de orden actualizado");
            OnOrderStatusChanged?.Invoke(data);
        });

        try
        {
            await _connection.StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to SignalR: {ex.Message}");
        }
    }

    public event Action<object>? OnNewOrderReceived;
    public event Action<object>? OnOrderStatusChanged;

    public async Task InvokeAsync(string methodName, object arg)
    {
        if (_connection?.State == HubConnectionState.Connected)
        {
            await _connection.InvokeAsync(methodName, arg);
        }
    }
}
