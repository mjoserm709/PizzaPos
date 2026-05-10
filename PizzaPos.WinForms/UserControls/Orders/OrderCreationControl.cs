using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.UserControls.Orders;

public partial class OrderCreationControl : UserControl
{
    private readonly string _token;
    private static readonly HttpClient _httpClient = new HttpClient();
    
    private BindingList<ProductModel> _productsCatalog = new();
    private BindingList<CartItem> _cart = new();
    private decimal _ivaRate = 0.15m;
    private CustomerModel? _selectedCustomer;

    public OrderCreationControl(string token)
    {
        InitializeComponent();
        _token = token;
        
        dgvProducts.AutoGenerateColumns = true;
        dgvCart.AutoGenerateColumns = false;
        
        ConfigureCartGrid();
        
        this.Load += async (s, e) => {
            await LoadIvaRate();
            await LoadProducts();
            LoadPaymentMethods();
        };
    }

    private void ConfigureCartGrid()
    {
        dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Producto", Width = 200, ReadOnly = true });
        dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Cant.", Width = 60 });
        dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Precio", Width = 80, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
        dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Total", HeaderText = "Total", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
        
        dgvCart.DataSource = _cart;
        _cart.ListChanged += (s, e) => UpdateTotals();
    }

    private async Task LoadIvaRate()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.GetAsync("http://localhost:5267/api/orders/iva-rate");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.GetProperty("success").GetBoolean())
                {
                    _ivaRate = doc.RootElement.GetProperty("rate").GetDecimal();
                    lblTax.Text = $"IVA ({_ivaRate:P0}): $0.00";
                }
            }
        }
        catch { /* Fallback to 13% */ }
    }

    private async Task LoadProducts()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.GetAsync("http://localhost:5267/api/security/catalog/products");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<ProductModel>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result?.Data != null)
                {
                    _productsCatalog = new BindingList<ProductModel>(result.Data);
                    dgvProducts.DataSource = _productsCatalog;
                    if (dgvProducts.Columns.Count > 0)
                    {
                        dgvProducts.Columns["Id"].Visible = false;
                        dgvProducts.Columns["Price"].DefaultCellStyle.Format = "C2";
                    }
                }
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error cargando productos: " + ex.Message); }
    }

    private void LoadPaymentMethods()
    {
        // Simplificado para este demo, en una app real se cargan de la API
        cmbPaymentMethod.Items.Add(new { Id = 1, Name = "Efectivo" });
        cmbPaymentMethod.Items.Add(new { Id = 2, Name = "Tarjeta" });
        cmbPaymentMethod.DisplayMember = "Name";
        cmbPaymentMethod.ValueMember = "Id";
        cmbPaymentMethod.SelectedIndex = 0;
    }

    private async void btnSearchCustomer_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSearchCustomer.Text)) return;

        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.GetAsync($"http://localhost:5267/api/customers/phone/{txtSearchCustomer.Text}");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<CustomerModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result.Success && result.Data != null)
                {
                    _selectedCustomer = result.Data;
                    lblCustomerInfo.Text = $"Cliente: {_selectedCustomer.FullName}";
                    lblCustomerInfo.ForeColor = System.Drawing.Color.Black;
                    
                    // Cargar direcciones
                    cmbAddress.DataSource = null;
                    cmbAddress.DataSource = _selectedCustomer.Addresses;
                    cmbAddress.DisplayMember = "Street";
                    
                    ToastNotification.Success("Cliente encontrado");
                }
            }
            else
            {
                _selectedCustomer = null;
                lblCustomerInfo.Text = "Cliente no encontrado. Se creará uno nuevo al finalizar.";
                lblCustomerInfo.ForeColor = System.Drawing.Color.DarkRed;
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error buscando cliente: " + ex.Message); }
    }

    private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var product = (ProductModel)dgvProducts.Rows[e.RowIndex].DataBoundItem;
        
        var existing = _cart.FirstOrDefault(c => c.ProductId == product.Id);
        if (existing != null)
        {
            existing.Quantity++;
            _cart.ResetBindings();
        }
        else
        {
            _cart.Add(new CartItem {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = 1,
                UnitPrice = product.Price
            });
        }
    }

    private void txtSearchProduct_TextChanged(object sender, EventArgs e)
    {
        var filter = txtSearchProduct.Text.ToLower();
        var filtered = _productsCatalog.Where(p => p.Name.ToLower().Contains(filter)).ToList();
        dgvProducts.DataSource = new BindingList<ProductModel>(filtered);
    }

    private void UpdateTotals()
    {
        decimal subtotal = _cart.Sum(c => c.Total);
        decimal tax = subtotal * _ivaRate;
        decimal total = subtotal + tax;

        lblSubtotal.Text = $"Subtotal: {subtotal:C2}";
        lblTax.Text = $"IVA ({_ivaRate:P0}): {tax:C2}";
        lblTotal.Text = $"TOTAL: {total:C2}";
    }

    private async void btnFinalize_Click(object sender, EventArgs e)
    {
        if (_cart.Count == 0)
        {
            ToastNotification.Warning("El carrito está vacío.");
            return;
        }

        if (_selectedCustomer == null)
        {
            ToastNotification.Warning("Por favor, busque un cliente por teléfono primero.");
            return;
        }

        var request = new
        {
            CustomerId = _selectedCustomer.Id,
            AddressId = (cmbAddress.SelectedItem as AddressModel)?.Id,
            PaymentMethodId = cmbPaymentMethod.SelectedIndex + 1,
            Items = _cart.Select(i => new
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList(),
            Notes = "Pedido desde WinForms"
        };

        try
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.PostAsync("http://localhost:5267/api/orders", content);
            
            if (res.IsSuccessStatusCode)
            {
                ToastNotification.Success("¡Pedido creado con éxito!");
                _cart.Clear();
                _selectedCustomer = null;
                lblCustomerInfo.Text = "No se ha seleccionado cliente";
                txtSearchCustomer.Clear();
            }
            else
            {
                ToastNotification.Error("Error al crear el pedido");
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error de conexión: " + ex.Message); }
    }
}

public class CartItem : INotifyPropertyChanged
{
    private int _quantity;
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity 
    { 
        get => _quantity; 
        set { _quantity = value; OnPropertyChanged(nameof(Quantity)); OnPropertyChanged(nameof(Total)); }
    }
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
