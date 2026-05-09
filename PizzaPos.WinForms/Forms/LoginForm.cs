using System.Text;
using System.Text.Json;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.Forms;

public partial class LoginForm : Form
{
    private static readonly HttpClient _httpClient = new HttpClient();

    private readonly string _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

    public LoginForm()
    {
        InitializeComponent();
    }

    private void LoginForm_Load(object sender, EventArgs e)
    {
        try
        {
            if (File.Exists(_configPath))
            {
                var json = File.ReadAllText(_configPath);
                var config = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                if (config != null && config.TryGetValue("RememberedUser", out string? username))
                {
                    txtUsername.Text = username;
                    chkRememberUser.Checked = true;
                    this.ActiveControl = txtPassword;
                }
            }
        }
        catch { /* Ignorar errores de carga de config */ }
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
                    // Guardar usuario si está marcado el check
                    try
                    {
                        if (chkRememberUser.Checked)
                        {
                            var config = new Dictionary<string, string> { { "RememberedUser", username } };
                            File.WriteAllText(_configPath, JsonSerializer.Serialize(config));
                        }
                        else if (File.Exists(_configPath))
                        {
                            File.Delete(_configPath);
                        }
                    }
                    catch { /* Ignorar errores de guardado de config */ }

                    var authResult = dynamicResult.Data;
                    var token = authResult.GetProperty("token").GetString();
                    var roles = authResult.GetProperty("roles").EnumerateArray().Select(r => r.GetString()!).ToList();
                    var fullName = authResult.GetProperty("fullName").GetString() ?? username;

                    ToastNotification.Success($"Bienvenido, {fullName}");

                    // Navegar al formulario principal
                    var mainForm = new MainForm(fullName, roles, token!);
                    
                    mainForm.FormClosed += (s, args) => {
                        if (mainForm.IsLoggingOut)
                        {
                            this.Show();
                            txtPassword.Clear();
                            lblStatus.Text = "";
                        }
                        else
                        {
                            Application.Exit();
                        }
                    };

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
