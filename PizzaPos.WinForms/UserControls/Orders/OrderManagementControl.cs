using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.UserControls.Orders;

public partial class OrderManagementControl : UserControl
{
    private readonly string _token;
    private static readonly HttpClient _httpClient = new HttpClient();
    private List<OrderResponseDto> _orders = new();

    public OrderManagementControl(string token)
    {
        InitializeComponent();
        _token = token;
        
        dgvOrders.AutoGenerateColumns = false;
        ConfigureGrid();

        this.Load += async (s, e) => {
            await LoadOrders();
            SetupSignalR();
        };
    }

    private void ConfigureGrid()
    {
        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OrderNumber", HeaderText = "Orden #", Width = 100 });
        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerName", HeaderText = "Cliente", Width = 150 });
        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StatusName", HeaderText = "Estado", Width = 100 });
        dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Total", HeaderText = "Total", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
        
        var btnAction = new DataGridViewButtonColumn { 
            Name = "ColAction",
            HeaderText = "Acción", 
            Text = "Siguiente Paso", 
            UseColumnTextForButtonValue = true,
            Width = 120 
        };
        dgvOrders.Columns.Add(btnAction);
    }

    private void SetupSignalR()
    {
        SignalRService.Instance.OnNewOrderReceived += async (order) => {
            // Refresh on UI thread
            this.Invoke(new Action(async () => await LoadOrders()));
        };

        SignalRService.Instance.OnOrderStatusChanged += (data) => {
            this.Invoke(new Action(async () => await LoadOrders()));
        };
    }

    private async Task LoadOrders()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            // Por ahora cargamos los "pendientes" y "en preparación"
            var res = await _httpClient.GetAsync("http://localhost:5267/api/orders/status/pendiente");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<OrderResponseDto>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                _orders = result?.Data ?? new();
                dgvOrders.DataSource = null;
                dgvOrders.DataSource = _orders;
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }

    private async void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvOrders.Columns[e.ColumnIndex].Name != "ColAction") return;

        var order = _orders[e.RowIndex];
        string nextStatus = order.StatusName.ToLower() switch {
            "pendiente" => "en_preparacion",
            "en_preparacion" => "listo",
            "listo" => "en_camino",
            "en_camino" => "entregado",
            _ => "entregado"
        };

        if (order.StatusName.ToLower() == "entregado") return;

        try
        {
            var content = new StringContent(JsonSerializer.Serialize(nextStatus), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.PutAsync($"http://localhost:5267/api/orders/{order.Id}/status", content);
            if (res.IsSuccessStatusCode)
            {
                ToastNotification.Success("Estado actualizado");
                await LoadOrders();
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }
}

public record OrderResponseDto(
    int Id,
    string OrderNumber,
    string CustomerName,
    string StatusName,
    decimal Total,
    DateTime CreatedAt
);
