using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace PizzaPos.WinForms;

public partial class UserManagementForm : Form
{
    private readonly string _token;
    private static readonly HttpClient _httpClient = new HttpClient();

    public UserManagementForm(string token)
    {
        InitializeComponent();
        _token = token;
    }

    private async void UserManagementForm_Load(object sender, EventArgs e)
    {
        await LoadRolesAndPermissions();
        await LoadUsers();
    }

    private async Task LoadRolesAndPermissions()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            var rolesJson = await _httpClient.GetStringAsync("http://localhost:5267/api/users/roles");
            var roles = JsonSerializer.Deserialize<List<string>>(rolesJson);
            clbRoles.Items.Clear();
            if (roles != null) foreach (var role in roles) clbRoles.Items.Add(role);

            var permsJson = await _httpClient.GetStringAsync("http://localhost:5267/api/users/permissions");
            var perms = JsonSerializer.Deserialize<List<string>>(permsJson);
            clbPermissions.Items.Clear();
            if (perms != null) foreach (var perm in perms) clbPermissions.Items.Add(perm);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error cargando datos: {ex.Message}");
        }
    }

    private async Task LoadUsers()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var usersJson = await _httpClient.GetStringAsync("http://localhost:5267/api/users");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<UserResponse>>(usersJson, options);

            dgvUsers.DataSource = users?.Select(u => new {
                u.Id,
                Usuario = u.Username,
                Activo = u.IsActive ? "Sí" : "No",
                Roles = string.Join(", ", u.Roles)
            }).ToList();
            
            if (dgvUsers.Columns.Count > 0)
            {
                dgvUsers.Columns["Roles"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error cargando usuarios: {ex.Message}");
        }
    }
    
    public record UserResponse(int Id, string Username, bool IsActive, List<string> Roles);

    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (txtNewPassword.Text != txtConfirmPassword.Text)
        {
            MessageBox.Show("Las contraseñas no coinciden.");
            return;
        }

        var rolesList = clbRoles.CheckedItems.Cast<string>().ToList();
        var permsList = clbPermissions.CheckedItems.Cast<string>().ToList();

        var userData = new
        {
            username = txtNewUsername.Text,
            password = txtNewPassword.Text,
            roleNames = rolesList,
            permissionNames = permsList
        };

        if (string.IsNullOrEmpty(userData.username) || string.IsNullOrEmpty(userData.password))
        {
            MessageBox.Show("Por favor complete todos los campos.");
            return;
        }

        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var content = new StringContent(JsonSerializer.Serialize(userData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5267/api/users", content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Usuario creado exitosamente.");
                txtNewUsername.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                for (int i = 0; i < clbRoles.Items.Count; i++) clbRoles.SetItemChecked(i, false);
                for (int i = 0; i < clbPermissions.Items.Count; i++) clbPermissions.SetItemChecked(i, false);
                await LoadUsers();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Error al crear usuario: {error}");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error de conexión: {ex.Message}");
        }
    }
}
