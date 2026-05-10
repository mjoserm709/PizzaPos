using System.Drawing;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.UserControls.Orders;

public partial class OrderManagementControl : UserControl
{
    private readonly string _token;
    private readonly List<string> _userRoles;
    private static readonly HttpClient _httpClient = new HttpClient();
    private List<OrderResponseDto> _orders = new();

    public OrderManagementControl(string token, List<string> roles)
    {
        InitializeComponent();
        _token = token;
        _userRoles = roles;
        
        this.Load += async (s, e) => {
            await LoadOrders();
            SetupSignalR();
        };
    }

    private void SetupSignalR()
    {
        SignalRService.Instance.OnNewOrderReceived += (order) => {
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
            var res = await _httpClient.GetAsync("http://localhost:5267/api/orders/active");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<OrderResponseDto>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                _orders = result?.Data ?? new();
                
                PopulateBoard();
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }

    private void PopulateBoard()
    {
        // Limpiar columnas
        flpPendiente.Controls.Clear();
        flpCocina.Controls.Clear();
        flpListo.Controls.Clear();
        flpCamino.Controls.Clear();

        foreach (var order in _orders)
        {
            var card = CreateOrderCard(order);
            int statusId = order.StatusId;

            if (statusId == 1 || statusId == 2) flpPendiente.Controls.Add(card);
            else if (statusId == 3) flpCocina.Controls.Add(card);
            else if (statusId == 4) flpListo.Controls.Add(card);
            else if (statusId == 5) flpCamino.Controls.Add(card);
        }
    }

    private Panel CreateOrderCard(OrderResponseDto order)
    {
        var panel = new Panel
        {
            Size = new Size(flpPendiente.Width - 25, 130),
            BackColor = Color.White,
            Margin = new Padding(5, 5, 5, 10),
            Padding = new Padding(10),
            BorderStyle = BorderStyle.FixedSingle
        };

        // Borde de color según estado
        var accentColor = GetStatusColor(order.StatusId);
        var pnlAccent = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = accentColor };
        panel.Controls.Add(pnlAccent);

        var lblOrder = new Label { 
            Text = order.OrderNumber, 
            Font = new Font("Segoe UI", 11F, FontStyle.Bold), 
            Location = new Point(15, 10),
            AutoSize = true 
        };
        
        var lblCustomer = new Label { 
            Text = order.CustomerName, 
            Font = new Font("Segoe UI", 9F), 
            Location = new Point(15, 35),
            Size = new Size(panel.Width - 30, 20)
        };

        var lblTotal = new Label { 
            Text = $"Total: {order.Total:C2}", 
            Font = new Font("Segoe UI", 10F, FontStyle.Bold), 
            Location = new Point(15, 60),
            ForeColor = Color.DarkGreen,
            AutoSize = true 
        };

        var btnNext = new Button {
            Text = GetNextActionText(order.StatusId),
            Location = new Point(15, 90),
            Size = new Size(panel.Width - 120, 30),
            BackColor = Color.FromArgb(46, 125, 50),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Enabled = CanUserAdvanceOrder(order.StatusId)
        };
        if (!btnNext.Enabled) btnNext.BackColor = Color.Gray;
        btnNext.FlatAppearance.BorderSize = 0;
        btnNext.Click += async (s, e) => await AdvanceOrder(order);

        // Botón Cancelar (Solo en etapas iniciales)
        if (order.StatusId <= 2)
        {
            var btnCancel = new Button {
                Text = "✖",
                Location = new Point(panel.Width - 100, 90),
                Size = new Size(35, 30),
                BackColor = Color.FromArgb(198, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += async (s, e) => await CancelOrder(order);
            panel.Controls.Add(btnCancel);
        }

        panel.Controls.Add(btnNext);
        panel.Controls.Add(lblTotal);
        panel.Controls.Add(lblCustomer);
        panel.Controls.Add(lblOrder);

        return panel;
    }

    private Color GetStatusColor(int statusId) => statusId switch {
        1 => Color.LightCoral, // Pendiente
        2 => Color.LightSkyBlue, // Confirmado
        3 => Color.Khaki, // En preparación
        4 => Color.LightGreen, // Listo
        5 => Color.Plum, // En camino
        _ => Color.Gray
    };

    private bool CanUserAdvanceOrder(int statusId)
    {
        bool isAdmin = _userRoles.Any(r => r.Equals("admin", StringComparison.OrdinalIgnoreCase) || r.Equals("cajero", StringComparison.OrdinalIgnoreCase));
        bool isChef = _userRoles.Any(r => r.Equals("cocinero", StringComparison.OrdinalIgnoreCase));
        bool isCourier = _userRoles.Any(r => r.Equals("repartidor", StringComparison.OrdinalIgnoreCase));

        if (isAdmin) return true;

        // Cocinero: Pendiente (1) -> Cocina (3) -> Listo (4)
        if (isChef && (statusId == 1 || statusId == 2 || statusId == 3)) return true;

        // Repartidor: Listo (4) -> En camino (5) -> Entregado (6)
        if (isCourier && (statusId == 4 || statusId == 5)) return true;

        return false;
    }

    private string GetNextActionText(int statusId) => statusId switch {
        1 => "Enviar a Cocina",
        2 => "Enviar a Cocina",
        3 => "Marcar como Listo",
        4 => "Despachar pedido",
        5 => "Marcar Entregado",
        _ => "Finalizar"
    };

    private async Task AdvanceOrder(OrderResponseDto order)
    {
        string nextStatus = order.StatusId switch {
            1 => "en_preparacion",
            2 => "en_preparacion",
            3 => "listo",
            4 => "en_camino",
            5 => "entregado",
            _ => "entregado"
        };

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

    private async Task CancelOrder(OrderResponseDto order)
    {
        if (MessageBox.Show($"¿Está seguro que desea cancelar la orden {order.OrderNumber}?", "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            return;

        try
        {
            var content = new StringContent(JsonSerializer.Serialize("cancelado"), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.PutAsync($"http://localhost:5267/api/orders/{order.Id}/status", content);
            if (res.IsSuccessStatusCode)
            {
                ToastNotification.Warning("Orden Cancelada");
                await LoadOrders();
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }
}

public class OrderResponseDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public string StatusCode { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}
