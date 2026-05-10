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
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120)); // Stats y Filtros
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

        // 2. PANEL SUPERIOR (Stats + Filtros)
        var topPanel = new FlowLayoutPanel { 
            Dock = DockStyle.Fill, 
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false
        };

        // Cards de Stats
        topPanel.Controls.Add(CreateStatCard("Ventas Totales", "$0.00", Color.FromArgb(46, 125, 50), out lblStatTotal));
        topPanel.Controls.Add(CreateStatCard("Pedidos", "0", Color.FromArgb(25, 118, 210), out lblStatCount));
        topPanel.Controls.Add(CreateStatCard("Cancelados", "0", Color.FromArgb(198, 40, 40), out lblStatCancelled));

        // Separador Espacial
        topPanel.Controls.Add(new Panel { Width = 30, Height = 10 });

        // Panel de Filtros (Card feel)
        var pnlFilters = new Panel { 
            Width = 550, 
            Height = 90, 
            BackColor = Color.White,
            Padding = new Padding(15)
        };
        pnlFilters.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, pnlFilters.Width, pnlFilters.Height, 15, 15));

        var lblF = new Label { Text = "Filtrar por Fecha y Búsqueda", Font = new Font("Segoe UI", 9F, FontStyle.Bold), Location = new Point(15, 10), AutoSize = true };
        dtpFrom = new DateTimePicker { Location = new Point(15, 35), Width = 110, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddDays(-7) };
        dtpTo = new DateTimePicker { Location = new Point(135, 35), Width = 110, Format = DateTimePickerFormat.Short, Value = DateTime.Today };
        txtSearch = new TextBox { Location = new Point(255, 35), Width = 150, PlaceholderText = "Buscar orden..." };
        
        btnSearch = new Button { 
            Text = "Actualizar", 
            Location = new Point(415, 32), 
            Size = new Size(110, 32),
            BackColor = Color.FromArgb(45, 45, 48),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold)
        };
        btnSearch.FlatAppearance.BorderSize = 0;
        btnSearch.Click += async (s, e) => await LoadHistory();

        pnlFilters.Controls.AddRange(new Control[] { lblF, dtpFrom, dtpTo, txtSearch, btnSearch });
        topPanel.Controls.Add(pnlFilters);

        mainLayout.Controls.Add(topPanel, 0, 1);

        // 3. GRID MODERNO
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
        mainLayout.Controls.Add(pnlGrid, 0, 2);

        this.Controls.Add(mainLayout);
    }

    private Panel CreateStatCard(string title, string value, Color accentColor, out Label lblValue)
    {
        var panel = new Panel { Width = 180, Height = 90, BackColor = Color.White, Margin = new Padding(0, 0, 15, 0) };
        panel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, 15, 15));

        var pnlAccent = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = accentColor };
        panel.Controls.Add(pnlAccent);

        var lblT = new Label { 
            Text = title, 
            Font = new Font("Segoe UI", 9F), 
            ForeColor = Color.Gray, 
            Location = new Point(15, 15), 
            AutoSize = true 
        };
        
        lblValue = new Label { 
            Text = value, 
            Font = new Font("Segoe UI Semibold", 16F), 
            ForeColor = Color.FromArgb(45, 45, 48), 
            Location = new Point(12, 35), 
            AutoSize = true 
        };

        panel.Controls.Add(lblValue);
        panel.Controls.Add(lblT);
        return panel;
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
                
                // Actualizar Stats - SOLO pedidos ENTREGADOS cuentan como ganancia
                decimal total = _orders.Where(o => o.StatusCode == "entregado").Sum(o => o.Total);
                int count = _orders.Count;
                int cancelled = _orders.Count(o => o.StatusCode == "cancelado");

                lblStatTotal.Text = total.ToString("C2");
                lblStatCount.Text = count.ToString();
                lblStatCancelled.Text = cancelled.ToString();

                dgvHistory.DataSource = _orders.Select(o => new {
                    Fecha = o.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    Orden = o.OrderNumber,
                    Cliente = o.CustomerName,
                    Estado = o.StatusName.ToUpper(),
                    Total = o.Total.ToString("C2")
                }).ToList();
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }
}
