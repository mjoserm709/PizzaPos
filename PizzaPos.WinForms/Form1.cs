using System.Text;
using System.Text.Json;

namespace PizzaPos.WinForms;

public partial class Form1 : Form
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public Form1()
    {
        InitializeComponent();
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        var username = txtUsername.Text;
        var password = txtPassword.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            lblStatus.Text = "Por favor ingrese usuario y contraseña";
            lblStatus.ForeColor = Color.Red;
            return;
        }

        lblStatus.Text = "Autenticando...";
        lblStatus.ForeColor = Color.Blue;

        try
        {
            var loginData = new { username, password };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5267/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var authResult = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
                
                var token = authResult.GetProperty("token").GetString();
                var role = authResult.GetProperty("role").GetString();

                lblStatus.Text = $"¡Bienvenido! Rol: {role}";
                lblStatus.ForeColor = Color.Green;

                // TODO: Guardar token y navegar al formulario principal
                MessageBox.Show($"Login Exitoso. Token: {token?.Substring(0, 10)}...", "Éxito");
            }
            else
            {
                lblStatus.Text = "Error: Usuario o contraseña incorrectos";
                lblStatus.ForeColor = Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Error de conexión: {ex.Message}";
            lblStatus.ForeColor = Color.Red;
        }
    }
}
