using System.Net.Http.Headers;
using System.Text.Json;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;
using PizzaPos.WinForms.Forms;

namespace PizzaPos.WinForms.UserControls.Admin;

public partial class CustomerManagementControl : UserControl
{
    private readonly string _token;
    private readonly List<string> _permissions;
    private static readonly HttpClient _httpClient = new HttpClient();
    private List<CustomerModel> _customers = new();

    public CustomerManagementControl(string token, List<string> permissions)
    {
        InitializeComponent();
        _token = token;
        _permissions = permissions;
        
        ApplyPermissions();
        this.Load += async (s, e) => await LoadCustomers();
    }

    private void ApplyPermissions()
    {
        btnNewCustomer.Enabled = _permissions.Contains("clientes.create");
        btnEditCustomer.Enabled = _permissions.Contains("clientes.update");
        btnToggleCustomer.Enabled = _permissions.Contains("clientes.delete");
    }

    private async Task LoadCustomers(string term = "")
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var url = string.IsNullOrWhiteSpace(term) 
                ? "http://localhost:5267/api/customers" 
                : $"http://localhost:5267/api/customers/search?term={term}";
                
            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<CustomerModel>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                _customers = result?.Data ?? new();
                dgvCustomers.DataSource = _customers;
                
                if (dgvCustomers.Columns.Count > 0)
                {
                    dgvCustomers.Columns["Id"].Visible = false;
                    dgvCustomers.Columns["FullName"].HeaderText = "Nombre Completo";
                    dgvCustomers.Columns["FullName"].Width = 250;
                    dgvCustomers.Columns["Phone"].HeaderText = "Teléfono";
                    dgvCustomers.Columns["IsActive"].HeaderText = "Activo";
                }
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }

    private async void txtSearch_TextChanged(object sender, EventArgs e)
    {
        await LoadCustomers(txtSearch.Text);
    }

    private void btnNewCustomer_Click(object sender, EventArgs e)
    {
        using var form = new CustomerForm(_token);
        if (form.ShowDialog() == DialogResult.OK)
        {
            _ = LoadCustomers(txtSearch.Text);
        }
    }

    private void btnEditCustomer_Click(object sender, EventArgs e)
    {
        if (dgvCustomers.SelectedRows.Count == 0) return;
        var customer = (CustomerModel)dgvCustomers.SelectedRows[0].DataBoundItem;
        
        using var form = new CustomerForm(_token, customer);
        if (form.ShowDialog() == DialogResult.OK)
        {
            _ = LoadCustomers(txtSearch.Text);
        }
    }

    private async void btnToggleCustomer_Click(object sender, EventArgs e)
    {
        if (dgvCustomers.SelectedRows.Count == 0) return;
        var customer = (CustomerModel)dgvCustomers.SelectedRows[0].DataBoundItem;

        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var res = await _httpClient.DeleteAsync($"http://localhost:5267/api/customers/{customer.Id}");
            if (res.IsSuccessStatusCode)
            {
                ToastNotification.Success("Estado del cliente actualizado");
                await LoadCustomers(txtSearch.Text);
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }
}
