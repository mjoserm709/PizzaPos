using System.Text;
using System.Text.Json;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.Forms;

public partial class LoginForm : Form
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public LoginForm()
    {
        InitializeComponent();
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        var username = txtUsername.Text;
        var password = txtPassword.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ToastNotification.Error("Ingrese usuario y contraseña");
            return;
        }

        lblStatus.Text = "Autenticando...";

        try
        {
            var loginData = new { username, password };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5267/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dynamicResult = JsonSerializer.Deserialize<DynamicResponse<JsonElement>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (dynamicResult != null && dynamicResult.Success)
                {
                    var authResult = dynamicResult.Data;
                    var token = authResult.GetProperty("token").GetString();
                    var roles = authResult.GetProperty("roles").EnumerateArray().Select(r => r.GetString()!).ToList();

                    ToastNotification.Success($"Bienvenido, {username}");

                    // Navegar al formulario principal
                    var mainForm = new MainForm(username, roles, token!);
                    this.Hide();
                    mainForm.Show();
                }
                else
                {
                    lblStatus.Text = dynamicResult?.Message ?? "Error desconocido";
                    lblStatus.ForeColor = Color.Red;
                }
            }
            else
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dynamicResult = JsonSerializer.Deserialize<DynamicResponse<JsonElement>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                string errorMessage = dynamicResult?.Message ?? "Usuario o contraseña incorrectos";
                if (dynamicResult?.Errors != null && dynamicResult.Errors.Any())
                {
                    errorMessage += " (" + string.Join(", ", dynamicResult.Errors) + ")";
                }
                
                ToastNotification.Error(errorMessage);
                lblStatus.Text = "";
            }
        }
        catch (Exception ex)
        {
            ToastNotification.Error($"Error de conexión: {ex.Message}");
            lblStatus.Text = "";
        }
    }
}
