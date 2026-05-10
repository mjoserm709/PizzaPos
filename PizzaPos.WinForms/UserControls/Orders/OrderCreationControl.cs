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
    private JsonElement? _pendingCompensation = null;

    public OrderCreationControl(string token)
    {
        InitializeComponent();
        _token = token;
        
        if (dgvCart != null)
        {
            dgvCart.AutoGenerateColumns = false;
            StyleGrids();
            ConfigureCartGrid();
            dgvCart.CellDoubleClick += (s, e) => RemoveItemFromCart(e.RowIndex);
        }
        
        this.Load += async (s, e) => {
            await LoadIvaRate();
            await LoadProducts();
            LoadPaymentMethods();
        };
    }

    private void StyleGrids()
    {
        dgvCart.BorderStyle = BorderStyle.None;
        dgvCart.BackgroundColor = Color.White;
        dgvCart.GridColor = Color.FromArgb(240, 240, 240);
        dgvCart.RowTemplate.Height = 35;
        dgvCart.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 251);
        dgvCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvCart.EnableHeadersVisualStyles = false;
        dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            var res = await _httpClient.GetAsync("http://localhost:5267/api/products");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<ProductModel>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result?.Data != null)
                {
                    _productsCatalog = new BindingList<ProductModel>(result.Data.Where(p => p.IsActive).ToList());
                    RenderProductTabs(_productsCatalog);
                }
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error cargando productos: " + ex.Message); }
    }

    private void RenderProductTabs(IEnumerable<ProductModel> products)
    {
        tabsProducts.TabPages.Clear();
        
        var grouped = products.GroupBy(p => p.Category?.Name ?? "General");
        
        foreach (var group in grouped)
        {
            var tabPage = new TabPage(group.Key);
            tabPage.BackColor = Color.White;
            
            var flow = new FlowLayoutPanel {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Padding = new Padding(10)
            };
            
            RenderCardsInFlow(group, flow);
            
            tabPage.Controls.Add(flow);
            tabsProducts.TabPages.Add(tabPage);
        }
    }

    private void RenderCardsInFlow(IEnumerable<ProductModel> products, FlowLayoutPanel container)
    {
        container.Controls.Clear();
        foreach (var product in products)
        {
            var card = new Panel {
                Size = new Size(130, 140),
                BackColor = Color.White,
                Margin = new Padding(5),
                Cursor = Cursors.Hand
            };
            card.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, card.Width, card.Height, 15, 15));

            var lblName = new Label {
                Text = product.Name,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 45,
                Padding = new Padding(5)
            };

            var lblPrice = new Label {
                Text = product.Price.ToString("C2"),
                Font = new Font("Segoe UI Semibold", 10F),
                ForeColor = Color.FromArgb(46, 125, 50),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30
            };

            var btnAdd = new Button {
                Text = "Añadir ➕",
                Dock = DockStyle.Bottom,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(232, 245, 233),
                ForeColor = Color.FromArgb(46, 125, 50),
                Height = 30,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold)
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            
            // Eventos
            Action addToCart = () => AddProductToCart(product);
            card.Click += (s, e) => addToCart();
            lblName.Click += (s, e) => addToCart();
            lblPrice.Click += (s, e) => addToCart();
            btnAdd.Click += (s, e) => addToCart();

            card.Controls.Add(btnAdd);
            card.Controls.Add(lblPrice);
            card.Controls.Add(lblName);
            container.Controls.Add(card);
        }
    }

    [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

    private void AddProductToCart(ProductModel product)
    {
        var existing = _cart.FirstOrDefault(c => c.ProductId == product.Id);
        if (existing != null)
        {
            existing.Quantity++;
            UpdateTotals();
            dgvCart.Refresh();
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

    private void LoadPaymentMethods()
    {
        // Simplificado para este demo, en una app real se cargan de la API
        cmbPaymentMethod.Items.Add(new { Id = 1, Name = "Efectivo" });
        cmbPaymentMethod.Items.Add(new { Id = 2, Name = "Tarjeta" });
        cmbPaymentMethod.DisplayMember = "Name";
        cmbPaymentMethod.ValueMember = "Id";
        cmbPaymentMethod.SelectedIndex = 0;
    }

    private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
    {
        searchTimer.Stop();
        
        // Si el texto coincide con el cliente seleccionado, no buscamos (evita bucle)
        if (_selectedCustomer != null && txtSearchCustomer.Text == _selectedCustomer.FullName)
        {
            lstCustomerResults.Visible = false;
            return;
        }

        if (txtSearchCustomer.Text.Length >= 2)
        {
            searchTimer.Start();
        }
        else
        {
            lstCustomerResults.Visible = false;
        }
    }

    private async void searchTimer_Tick(object sender, EventArgs e)
    {
        searchTimer.Stop();
        await PerformCustomerSearch(txtSearchCustomer.Text);
    }

    private async Task PerformCustomerSearch(string term)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.GetAsync($"http://localhost:5267/api/customers/search?term={term}");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<CustomerModel>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result?.Data != null && result.Data.Any())
                {
                    var displayList = result.Data.Select(c => new CustomerSearchItem { 
                        Display = $"{c.FullName} | {c.Phone}", 
                        Data = c 
                    }).ToList();

                    lstCustomerResults.DataSource = displayList;
                    lstCustomerResults.DisplayMember = "Display";
                    lstCustomerResults.ValueMember = "Data";
                    
                    lstCustomerResults.Visible = true;
                    lstCustomerResults.BringToFront();
                }
                else
                {
                    lstCustomerResults.Visible = false;
                }
            }
        }
        catch { lstCustomerResults.Visible = false; }
    }

    private void lstCustomerResults_DoubleClick(object sender, EventArgs e)
    {
        SelectCustomer();
    }

    private void lstCustomerResults_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SelectCustomer();
        }
    }

    private async void SelectCustomer()
    {
        if (lstCustomerResults.SelectedItem is CustomerSearchItem selected)
        {
            _selectedCustomer = selected.Data;
            
            lstCustomerResults.Visible = false;
            
            txtSearchCustomer.Text = _selectedCustomer.FullName;
            lblCustomerInfo.Text = $"👤 {_selectedCustomer.FullName} | 📞 {_selectedCustomer.Phone}";
            lblCustomerInfo.ForeColor = System.Drawing.Color.FromArgb(46, 125, 50);
            
            cmbAddress.DataSource = null;
            cmbAddress.DataSource = _selectedCustomer.Addresses;
            cmbAddress.DisplayMember = "Street";
            
            await CheckCustomerCompensation(_selectedCustomer.Id);
            
            ToastNotification.Success("Cliente seleccionado");
            
            this.ActiveControl = cmbAddress;
        }
    }

    private async Task CheckCustomerCompensation(int customerId)
    {
        try
        {
            _pendingCompensation = null;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.GetAsync($"http://localhost:5267/api/customers/{customerId}/compensation");
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                if (root.TryGetProperty("data", out var data) && data.ValueKind != JsonValueKind.Null)
                {
                    _pendingCompensation = data;
                    string desc = data.GetProperty("description").GetString() ?? "Compensación";
                    decimal discount = data.GetProperty("discountAmount").GetDecimal();
                    
                    MessageBox.Show($"🎁 ¡ATENCIÓN! Este cliente tiene una COMPENSACIÓN PENDIENTE:\n\n{desc}\n\nSe aplicará un {discount:P0} de descuento automáticamente.", 
                        "Beneficio de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    UpdateTotals();
                }
            }
        }
        catch { /* Silencioso */ }
    }

    private async void btnSearchCustomer_Click(object sender, EventArgs e)
    {
        await PerformCustomerSearch(txtSearchCustomer.Text);
    }





    private void UpdateTotals()
    {
        if (lblSubtotal == null || lblTax == null || lblTotal == null) return;

        decimal subtotal = _cart.Sum(c => c.Total);
        decimal tax = subtotal * _ivaRate;
        decimal total = subtotal + tax;

        if (_pendingCompensation != null)
        {
            decimal discountRate = _pendingCompensation.Value.GetProperty("discountAmount").GetDecimal();
            decimal discount = total * discountRate;
            total -= discount;
            lblTotal.Text = $"TOTAL: {total:C2} (Desc. Aplicado)";
            lblTotal.ForeColor = System.Drawing.Color.FromArgb(198, 40, 40);
        }
        else
        {
            lblTotal.Text = $"TOTAL: {total:C2}";
            lblTotal.ForeColor = System.Drawing.Color.Black;
        }

        lblSubtotal.Text = $"Subtotal: {subtotal:C2}";
        lblTax.Text = $"IVA ({_ivaRate:P0}): {tax:C2}";
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
            PaymentMethodId = (cmbPaymentMethod.SelectedItem as dynamic)?.Id ?? 1,
            Items = _cart.Select(i => new
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList(),
            Notes = txtNotes.Text
        };

        try
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.PostAsync("http://localhost:5267/api/orders", content);
            
            if (res.IsSuccessStatusCode)
            {
                if (_pendingCompensation != null)
                {
                    await _httpClient.PostAsync($"http://localhost:5267/api/customers/{_selectedCustomer.Id}/redeem-compensation", null);
                    _pendingCompensation = null;
                }

                ToastNotification.Success("¡Pedido creado con éxito!");
                _cart.Clear();
                _selectedCustomer = null;
                lblCustomerInfo.Text = "No se ha seleccionado cliente";
                txtSearchCustomer.Clear();
                UpdateTotals();
            }
            else
            {
                ToastNotification.Error("Error al crear el pedido");
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error de conexión: " + ex.Message); }
    }

    private void RemoveItemFromCart(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= _cart.Count) return;

        var item = _cart[rowIndex];
        if (item.Quantity > 1)
        {
            item.Quantity--;
        }
        else
        {
            _cart.RemoveAt(rowIndex);
        }
        
        dgvCart.Refresh();
        UpdateTotals();
    }

    private void txtSearchProduct_TextChanged(object sender, EventArgs e)
    {
        string term = txtSearchProduct.Text.ToLower();
        
        // Si hay búsqueda, mostramos todo en una pestaña temporal o simplemente filtramos en cada flow
        // Opción: Filtrar en la pestaña actual o regenerar todo
        var filtered = _productsCatalog.Where(p => 
            p.Name.ToLower().Contains(term) || 
            (p.Description?.ToLower().Contains(term) ?? false)
        ).ToList();
        
        RenderProductTabs(filtered);
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

public class CustomerSearchItem
{
    public string Display { get; set; } = string.Empty;
    public CustomerModel Data { get; set; } = null!;
}
