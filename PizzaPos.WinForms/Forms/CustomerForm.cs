using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.ComponentModel;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.Forms;

public partial class CustomerForm : Form
{
    private readonly string _token;
    private readonly CustomerModel? _customer;
    private static readonly HttpClient _httpClient = new HttpClient();
    private BindingList<AddressModel> _addresses = new();

    public CustomerForm(string token, CustomerModel? customer = null)
    {
        InitializeComponent();
        _token = token;
        _customer = customer;
        
        ConfigureAddressesGrid();
        
        if (_customer != null)
        {
            this.Text = "Editar Cliente";
            txtFullName.Text = _customer.FullName;
            txtPhone.Text = _customer.Phone;
            txtEmail.Text = _customer.Email;
            
            // Cargar direcciones existentes
            foreach (var addr in _customer.Addresses)
            {
                _addresses.Add(addr);
            }
        }
        else
        {
            this.Text = "Nuevo Cliente";
        }
    }

    private void ConfigureAddressesGrid()
    {
        dgvAddresses.AutoGenerateColumns = false;
        dgvAddresses.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Street", HeaderText = "Calle/Dirección", Width = 180 });
        dgvAddresses.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Sector", HeaderText = "Sector", Width = 100 });
        dgvAddresses.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Reference", HeaderText = "Referencia", Width = 130 });
        
        dgvAddresses.DataSource = _addresses;
        
        btnAddAddress.Click += (s, e) => _addresses.Add(new AddressModel());
        btnRemoveAddress.Click += (s, e) => {
            if (dgvAddresses.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvAddresses.SelectedRows)
                {
                    if (!row.IsNewRow) _addresses.Remove((AddressModel)row.DataBoundItem);
                }
            }
        };
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
        {
            ToastNotification.Warning("Nombre y Teléfono son obligatorios.");
            return;
        }

        // Limpiar direcciones vacías antes de enviar
        var finalAddresses = _addresses.Where(a => !string.IsNullOrWhiteSpace(a.Street)).ToList();

        var data = new {
            FullName = txtFullName.Text,
            Phone = txtPhone.Text,
            Email = txtEmail.Text,
            Addresses = finalAddresses
        };

        try
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            HttpResponseMessage res;
            if (_customer == null)
                res = await _httpClient.PostAsync("http://localhost:5267/api/customers", content);
            else
                res = await _httpClient.PutAsync($"http://localhost:5267/api/customers/{_customer.Id}", content);

            if (res.IsSuccessStatusCode)
            {
                ToastNotification.Success("Cliente y direcciones guardados correctamente");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                var errorBody = await res.Content.ReadAsStringAsync();
                ToastNotification.Error("Error al guardar: " + errorBody);
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error de red: " + ex.Message); }
    }
}
