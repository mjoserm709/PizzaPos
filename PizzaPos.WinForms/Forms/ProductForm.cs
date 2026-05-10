using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.Forms;

public partial class ProductForm : Form
{
    private readonly string _token;
    private readonly ProductModel? _product;
    private static readonly HttpClient _httpClient = new HttpClient();

    public ProductForm(string token, ProductModel? product = null)
    {
        InitializeComponent();
        _token = token;
        _product = product;
        
        if (_product != null)
        {
            this.Text = "Editar Producto";
            txtName.Text = _product.Name;
            numPrice.Value = _product.Price;
            txtDescription.Text = _product.Description;
        }
        else
        {
            this.Text = "Nuevo Producto";
        }
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            ToastNotification.Warning("El nombre del producto es obligatorio.");
            return;
        }

        var data = new {
            Name = txtName.Text,
            Price = numPrice.Value,
            Description = txtDescription.Text,
            CategoryId = 1 // Por ahora fijo a la categoría principal
        };

        try
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            HttpResponseMessage res;
            if (_product == null)
                res = await _httpClient.PostAsync("http://localhost:5267/api/products", content);
            else
                res = await _httpClient.PutAsync($"http://localhost:5267/api/products/{_product.Id}", content);

            if (res.IsSuccessStatusCode)
            {
                ToastNotification.Success("Producto guardado correctamente");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                ToastNotification.Error("Error al guardar producto");
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error: " + ex.Message); }
    }
}
