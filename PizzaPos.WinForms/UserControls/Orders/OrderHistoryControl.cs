using System.Drawing;
using System.Net.Http.Headers;
using System.Text.Json;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.UserControls.Orders;

public partial class OrderHistoryControl : UserControl
{
    private readonly string _token;
    private static readonly HttpClient _httpClient = new HttpClient();
    private List<OrderResponseDto> _orders = new();

    private DataGridView dgvHistory;
    private DateTimePicker dtpFrom;
    private DateTimePicker dtpTo;
    private TextBox txtSearch;
    private Button btnSearch;
    
    private Label lblStatTotal;
    private Label lblStatCount;
    private Label lblStatCancelled;
    private Label lblStatPending;
    private Label lblStatKitchen;
    private Label lblStatDelivery;

    public OrderHistoryControl(string token)
    {
        _token = token;
        SetupUI();
        this.Load += async (s, e) => await LoadHistory();
    }

    private void SetupUI()
    {
        this.Dock = DockStyle.Fill;
        this.BackColor = Color.FromArgb(245, 247, 251); // Color de fondo moderno

        var mainLayout = new TableLayoutPanel { 
            Dock = DockStyle.Fill, 
            RowCount = 3, 
            ColumnCount = 1,
            Padding = new Padding(25)
        };
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Título
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100)); // Cards
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Filtros
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Grid

        // 1. TÍTULO
        var lblTitle = new Label { 
            Text = "Panel de Auditoría de Ventas", 
            Font = new Font("Segoe UI Semibold", 20F), 
            ForeColor = Color.FromArgb(45, 45, 48),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };
        mainLayout.Controls.Add(lblTitle, 0, 0);

        // 2. PANEL DE CARDS (Tarjetas de estado)
        var topPanel = new FlowLayoutPanel { 
            Dock = DockStyle.Fill, 
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false
        };

        // Cards de Stats
        topPanel.Controls.Add(CreateStatCard("Ventas Totales", "L0.00", Color.FromArgb(46, 125, 50), out lblStatTotal, null));
        topPanel.Controls.Add(CreateStatCard("Entregados", "0", Color.FromArgb(25, 118, 210), out lblStatCount, "entregado"));
        topPanel.Controls.Add(CreateStatCard("Pendientes", "0", Color.FromArgb(255, 160, 0), out lblStatPending, "pendiente"));
        topPanel.Controls.Add(CreateStatCard("En Cocina", "0", Color.FromArgb(123, 31, 162), out lblStatKitchen, "en_preparacion"));
        topPanel.Controls.Add(CreateStatCard("En Camino", "0", Color.FromArgb(0, 151, 167), out lblStatDelivery, "en_camino"));
        topPanel.Controls.Add(CreateStatCard("Cancelados", "0", Color.FromArgb(198, 40, 40), out lblStatCancelled, "cancelado"));

        mainLayout.Controls.Add(topPanel, 0, 1);

        // 3. PANEL DE FILTROS (Debajo de las cards)
        var pnlFilters = new Panel { 
            Dock = DockStyle.Fill, 
            Height = 50, 
            BackColor = Color.Transparent,
            Padding = new Padding(0, 5, 0, 5)
        };

        var filterContainer = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };

        var lblF = new Label { Text = "Filtrar por Fecha:", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Margin = new Padding(0, 8, 5, 0), AutoSize = true };
        dtpFrom = new DateTimePicker { Width = 110, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddDays(-7) };
        dtpTo = new DateTimePicker { Width = 110, Format = DateTimePickerFormat.Short, Value = DateTime.Today };
        
        var lblS = new Label { Text = "Búsqueda:", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Margin = new Padding(20, 8, 5, 0), AutoSize = true };
        txtSearch = new TextBox { Width = 200, PlaceholderText = "Nombre cliente u orden..." };
        
        btnSearch = new Button { 
            Text = "Actualizar Vista 🔄", 
            Width = 150,
            Height = 30,
            BackColor = Color.FromArgb(45, 45, 48),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Margin = new Padding(15, 0, 0, 0)
        };
        btnSearch.FlatAppearance.BorderSize = 0;
        btnSearch.Click += async (s, e) => await LoadHistory();

        filterContainer.Controls.AddRange(new Control[] { lblF, dtpFrom, dtpTo, lblS, txtSearch, btnSearch });
        pnlFilters.Controls.Add(filterContainer);

        mainLayout.Controls.Add(pnlFilters, 0, 2);

        // 4. GRID MODERNO
        var pnlGrid = new Panel { 
            Dock = DockStyle.Fill, 
            BackColor = Color.White,
            Padding = new Padding(1)
        };
        
        dgvHistory = new DataGridView {
            Dock = DockStyle.Fill,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            ReadOnly = true,
            AllowUserToAddRows = false,
            RowHeadersVisible = false,
            GridColor = Color.FromArgb(240, 240, 240),
            RowTemplate = { Height = 45 },
            EnableHeadersVisualStyles = false
        };
        
        dgvHistory.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle {
            BackColor = Color.FromArgb(245, 247, 251),
            ForeColor = Color.FromArgb(100, 100, 100),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            Padding = new Padding(10, 0, 0, 0)
        };

        dgvHistory.DefaultCellStyle = new DataGridViewCellStyle {
            Font = new Font("Segoe UI", 10F),
            SelectionBackColor = Color.FromArgb(232, 240, 254),
            SelectionForeColor = Color.FromArgb(25, 118, 210),
            Padding = new Padding(10, 0, 0, 0)
        };

        pnlGrid.Controls.Add(dgvHistory);
        mainLayout.Controls.Add(pnlGrid, 0, 3);

        this.Controls.Add(mainLayout);
    }

    private Panel CreateStatCard(string title, string value, Color accentColor, out Label lblValue, string? filterStatus)
    {
        var panel = new Panel { 
            Width = 145, 
            Height = 90, 
            BackColor = Color.White, 
            Margin = new Padding(0, 0, 10, 0),
            Cursor = Cursors.Hand
        };
        panel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, 15, 15));

        // Evento de filtrado al hacer clic en la tarjeta
        panel.Click += (s, e) => ApplyLocalFilter(filterStatus);

        var pnlAccent = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = accentColor, Enabled = false };
        panel.Controls.Add(pnlAccent);

        var lblT = new Label { 
            Text = title, 
            Font = new Font("Segoe UI", 8F), 
            ForeColor = Color.Gray, 
            Location = new Point(15, 15), 
            AutoSize = true,
            Enabled = false
        };
        
        lblValue = new Label { 
            Text = value, 
            Font = new Font("Segoe UI Semibold", 13F), 
            ForeColor = Color.FromArgb(45, 45, 48), 
            Location = new Point(12, 35), 
            AutoSize = true,
            Enabled = false
        };

        panel.Controls.Add(lblValue);
        panel.Controls.Add(lblT);

        // Asegurar que el clic funcione en los labels también
        lblT.Click += (s, e) => ApplyLocalFilter(filterStatus);
        lblValue.Click += (s, e) => ApplyLocalFilter(filterStatus);

        return panel;
    }

    private void ApplyLocalFilter(string? statusCode)
    {
        var filtered = string.IsNullOrEmpty(statusCode) 
            ? _orders 
            : _orders.Where(o => o.StatusCode == statusCode || (statusCode == "pendiente" && o.StatusCode == "confirmado") || (statusCode == "en_preparacion" && o.StatusCode == "listo")).ToList();

        dgvHistory.DataSource = filtered.Select(o => {
            var displayDate = (o.StatusCode == "entregado" || o.StatusCode == "cancelado") ? o.UpdatedAt : o.CreatedAt;
            return new {
                Fecha = displayDate.ToString("dd/MM/yyyy HH:mm"),
                Orden = o.OrderNumber,
                Cliente = o.CustomerName,
                Estado = o.StatusName.ToUpper(),
                Total = o.Total.ToString("C2")
            };
        }).ToList();
    }

    [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

    private async Task LoadHistory()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            string url = $"http://localhost:5267/api/orders/history?startDate={dtpFrom.Value:yyyy-MM-dd}&endDate={dtpTo.Value:yyyy-MM-dd}&searchTerm={txtSearch.Text}";
            
            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<OrderResponseDto>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                _orders = result?.Data ?? new();
                
                // Actualizar Stats
                decimal total = _orders.Where(o => o.StatusCode == "entregado").Sum(o => o.Total);
                int delivered = _orders.Count(o => o.StatusCode == "entregado");
                int pending = _orders.Count(o => o.StatusCode == "pendiente" || o.StatusCode == "confirmado");
                int kitchen = _orders.Count(o => o.StatusCode == "en_preparacion" || o.StatusCode == "listo");
                int delivery = _orders.Count(o => o.StatusCode == "en_camino");
                int cancelled = _orders.Count(o => o.StatusCode == "cancelado");

                lblStatTotal.Text = total.ToString("C2");
                lblStatCount.Text = delivered.ToString();
                lblStatPending.Text = pending.ToString();
                lblStatKitchen.Text = kitchen.ToString();
                lblStatDelivery.Text = delivery.ToString();
                lblStatCancelled.Text = cancelled.ToString();

                dgvHistory.DataSource = _orders.Select(o => {
                    // Usar UpdatedAt para Entregados/Cancelados, sino CreatedAt
                    var displayDate = (o.StatusCode == "entregado" || o.StatusCode == "cancelado") ? o.UpdatedAt : o.CreatedAt;
                    return new {
                        Fecha = displayDate.ToString("dd/MM/yyyy HH:mm"),
                        Orden = o.OrderNumber,
                        Cliente = o.CustomerName,
                        Estado = o.StatusName.ToUpper(),
                        Total = o.Total.ToString("C2")
                    };
                }).ToList();
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }
}
