using System.Net.Http.Headers;
using System.Text.Json;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;
using PizzaPos.WinForms.Forms;

namespace PizzaPos.WinForms.UserControls.Admin;

public partial class ProductManagementControl : UserControl
{
    private readonly string _token;
    private readonly List<string> _permissions;
    private static readonly HttpClient _httpClient = new HttpClient();

    public ProductManagementControl(string token, List<string> permissions)
    {
        InitializeComponent();
        _token = token;
        _permissions = permissions;
        
        ApplyPermissions();
        this.Load += async (s, e) => await LoadProducts();
    }

    private void ApplyPermissions()
    {
        btnNewProduct.Enabled = _permissions.Contains("productos.manage");
        btnEditProduct.Enabled = _permissions.Contains("productos.manage");
        btnToggleProduct.Enabled = _permissions.Contains("productos.manage");
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
                dgvProducts.DataSource = result?.Data ?? new();
                
                if (dgvProducts.Columns.Count > 0)
                {
                    dgvProducts.Columns["Id"].Visible = false;
                    dgvProducts.Columns["CategoryId"].Visible = false;
                    dgvProducts.Columns["Category"].Visible = false;
                    dgvProducts.Columns["SizeId"].Visible = false;
                    dgvProducts.Columns["Size"].Visible = false;
                    dgvProducts.Columns["Description"].Width = 200;
                    
                    dgvProducts.Columns["Name"].HeaderText = "Producto";
                    dgvProducts.Columns["Name"].Width = 250;
                    dgvProducts.Columns["Price"].HeaderText = "Precio";
                    dgvProducts.Columns["Price"].DefaultCellStyle.Format = "C2";
                    dgvProducts.Columns["IsActive"].HeaderText = "Activo";
                }
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }

    private void btnNewProduct_Click(object sender, EventArgs e)
    {
        using var form = new ProductForm(_token);
        if (form.ShowDialog() == DialogResult.OK)
        {
            _ = LoadProducts();
        }
    }

    private void btnEditProduct_Click(object sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0) return;
        var product = (ProductModel)dgvProducts.SelectedRows[0].DataBoundItem;
        
        using var form = new ProductForm(_token, product);
        if (form.ShowDialog() == DialogResult.OK)
        {
            _ = LoadProducts();
        }
    }

    private async void btnToggleProduct_Click(object sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0) return;
        var product = (ProductModel)dgvProducts.SelectedRows[0].DataBoundItem;

        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.DeleteAsync($"http://localhost:5267/api/products/{product.Id}");
            if (res.IsSuccessStatusCode)
            {
                ToastNotification.Success("Estado del producto actualizado");
                await LoadProducts();
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }
}
