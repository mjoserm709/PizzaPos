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
        
        this.Load += async (s, e) => await LoadFormData();
    }

    private async Task LoadFormData()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            // Cargar Categorías
            var catRes = await _httpClient.GetAsync("http://localhost:5267/api/products/categories");
            if (catRes.IsSuccessStatusCode)
            {
                var json = await catRes.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<ProductCategoryModel>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                cmbCategory.DataSource = result?.Data ?? new();
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "Id";
            }

            // Cargar Tamaños
            var sizeRes = await _httpClient.GetAsync("http://localhost:5267/api/products/sizes");
            if (sizeRes.IsSuccessStatusCode)
            {
                var json = await sizeRes.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<ProductSizeModel>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var sizes = result?.Data ?? new();
                sizes.Insert(0, new ProductSizeModel { Id = 0, Name = "N/A" });
                cmbSize.DataSource = sizes;
                cmbSize.DisplayMember = "Name";
                cmbSize.ValueMember = "Id";
            }

            if (_product != null)
            {
                this.Text = "Editar Producto";
                txtName.Text = _product.Name;
                numPrice.Value = _product.Price;
                txtDescription.Text = _product.Description;
                cmbCategory.SelectedValue = _product.CategoryId;
                cmbSize.SelectedValue = _product.SizeId ?? 0;
            }
            else
            {
                this.Text = "Nuevo Producto";
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error al cargar datos: " + ex.Message); }
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            ToastNotification.Warning("El nombre del producto es obligatorio.");
            return;
        }

        var sizeId = (int)cmbSize.SelectedValue!;
        var data = new {
            Name = txtName.Text,
            Price = numPrice.Value,
            Description = txtDescription.Text,
            CategoryId = (int)cmbCategory.SelectedValue!,
            SizeId = sizeId > 0 ? (int?)sizeId : null
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
