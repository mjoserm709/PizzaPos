using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.UserControls.Admin;

public partial class UserManagementControl : UserControl
{
    private readonly string _token;
    private static readonly HttpClient _httpClient = new HttpClient();
    private int? _selectedUserId;
    private List<UserResponse> _usersList = new();

    public UserManagementControl(string token)
    {
        InitializeComponent();
        _token = token;
        txtIdentity.TextChanged += txtIdentity_TextChanged;
        
        // Load data when control is created
        this.Load += async (s, e) => {
            await LoadRolesAndPermissions();
            await LoadUsers();
        };
    }

    private async Task LoadRolesAndPermissions()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            var response = await _httpClient.GetAsync("http://localhost:5267/api/users/roles");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dynamicResult = JsonSerializer.Deserialize<DynamicResponse<List<string>>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (dynamicResult != null && dynamicResult.Success && dynamicResult.Data != null)
                {
                    clbRoles.Items.Clear();
                    foreach (var role in dynamicResult.Data) clbRoles.Items.Add(role);
                }
            }

            response = await _httpClient.GetAsync("http://localhost:5267/api/users/permissions");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dynamicResult = JsonSerializer.Deserialize<DynamicResponse<List<string>>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (dynamicResult != null && dynamicResult.Success && dynamicResult.Data != null)
                {
                    clbPermissions.Items.Clear();
                    foreach (var perm in dynamicResult.Data) clbPermissions.Items.Add(perm);
                }
            }
        }
        catch (Exception ex)
        {
            ToastNotification.Error($"Error cargando datos: {ex.Message}");
        }
    }

    private async Task LoadUsers()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await _httpClient.GetAsync("http://localhost:5267/api/users");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dynamicResult = JsonSerializer.Deserialize<DynamicResponse<List<UserResponse>>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (dynamicResult != null && dynamicResult.Success && dynamicResult.Data != null)
                {
                    _usersList = dynamicResult.Data;
                    dgvUsers.DataSource = _usersList.Select(u => new {
                        u.Id,
                        Usuario = u.Username,
                        Nombre = u.FullName,
                        Identidad = u.IdentityNumber,
                        Activo = u.IsActive ? "Sí" : "No",
                        Creado = u.CreatedAt.ToString("g"),
                        CreadoPor = u.CreatedBy,
                        Modificado = u.UpdatedAt?.ToString("g") ?? "-",
                        ModificadoPor = u.UpdatedBy ?? "-",
                        Roles = string.Join(", ", u.Roles)
                    }).ToList();
                    
                    if (dgvUsers.Columns.Count > 0)
                    {
                        dgvUsers.Columns["Roles"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ToastNotification.Error($"Error cargando usuarios: {ex.Message}");
        }
    }
    
    public record UserResponse(int Id, string Username, string FullName, string IdentityNumber, bool IsActive, DateTime CreatedAt, string CreatedBy, DateTime? UpdatedAt, string? UpdatedBy, List<string> Roles, List<string> Permissions);

    private void dgvUsers_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvUsers.CurrentRow != null && dgvUsers.CurrentRow.Selected)
        {
            var id = (int)dgvUsers.CurrentRow.Cells["Id"].Value;
            var user = _usersList.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _selectedUserId = user.Id;
                txtNewUsername.Text = user.Username;
                txtFullName.Text = user.FullName;
                txtIdentity.Text = user.IdentityNumber;
                chkIsActive.Checked = user.IsActive;
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                
                for (int i = 0; i < clbRoles.Items.Count; i++)
                    clbRoles.SetItemChecked(i, user.Roles.Contains(clbRoles.Items[i]?.ToString() ?? string.Empty));
                
                for (int i = 0; i < clbPermissions.Items.Count; i++)
                    clbPermissions.SetItemChecked(i, user.Permissions.Contains(clbPermissions.Items[i]?.ToString() ?? string.Empty));

                lblTitle.Text = "Editar Usuario";
                btnSave.Text = "Actualizar Usuario";
            }
        }
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedUserId = null;
        txtNewUsername.Clear();
        txtFullName.Clear();
        txtIdentity.Clear();
        chkIsActive.Checked = true;
        txtNewPassword.Clear();
        txtConfirmPassword.Clear();
        for (int i = 0; i < clbRoles.Items.Count; i++) clbRoles.SetItemChecked(i, false);
        for (int i = 0; i < clbPermissions.Items.Count; i++) clbPermissions.SetItemChecked(i, false);
        lblTitle.Text = "Crear Nuevo Usuario";
        btnSave.Text = "Guardar Usuario";
        if (dgvUsers.CurrentRow != null) dgvUsers.CurrentRow.Selected = false;
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (txtNewPassword.Text != txtConfirmPassword.Text)
        {
            ToastNotification.Error("Las contraseñas no coinciden.");
            return;
        }

        var rolesList = clbRoles.CheckedItems.Cast<string>().ToList();
        var permsList = clbPermissions.CheckedItems.Cast<string>().ToList();

        var userData = new
        {
            id = _selectedUserId ?? 0,
            username = txtNewUsername.Text,
            password = txtNewPassword.Text,
            fullName = txtFullName.Text,
            identityNumber = txtIdentity.Text,
            isActive = chkIsActive.Checked,
            roleNames = rolesList,
            permissionNames = permsList
        };

        if (string.IsNullOrEmpty(userData.username) || 
            string.IsNullOrEmpty(userData.fullName) || 
            string.IsNullOrEmpty(userData.identityNumber) ||
            (_selectedUserId == null && string.IsNullOrEmpty(userData.password)))
        {
            ToastNotification.Error("Complete todos los campos requeridos.");
            return;
        }

        if (userData.identityNumber.Length != 15)
        {
            ToastNotification.Error("Identidad debe tener 15 caracteres (XXXX-XXXX-XXXXX).");
            return;
        }

        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var content = new StringContent(JsonSerializer.Serialize(userData), Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            if (_selectedUserId == null)
            {
                response = await _httpClient.PostAsync("http://localhost:5267/api/users", content);
            }
            else
            {
                response = await _httpClient.PutAsync("http://localhost:5267/api/users", content);
            }

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dynamicResult = JsonSerializer.Deserialize<DynamicResponse<string>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                string action = _selectedUserId == null ? "creado" : "actualizado";
                ToastNotification.Success($"El usuario '{userData.username}' ha sido {action} correctamente.");
                
                ClearForm();
                await LoadUsers();
            }
            else
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dynamicResult = JsonSerializer.Deserialize<DynamicResponse<string>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                string errorMessage = dynamicResult?.Message ?? "Error en la operación";
                if (dynamicResult?.Errors != null && dynamicResult.Errors.Any())
                {
                    errorMessage = string.Join(" | ", dynamicResult.Errors);
                }
                
                ToastNotification.Error(errorMessage);
            }
        }
        catch (Exception ex)
        {
            ToastNotification.Error($"Error de conexión: {ex.Message}");
        }
    }

    private void txtIdentity_TextChanged(object sender, EventArgs e)
    {
        string text = txtIdentity.Text.Replace("-", "");
        if (text.Length > 13) text = text.Substring(0, 13);
        
        string formatted = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (i == 4 || i == 8) formatted += "-";
            formatted += text[i];
        }

        if (txtIdentity.Text != formatted)
        {
            int selectionStart = txtIdentity.SelectionStart;
            int oldLength = txtIdentity.Text.Length;
            txtIdentity.Text = formatted;
            
            // Adjust cursor position
            int newLength = formatted.Length;
            int newSelectionStart = selectionStart + (newLength - oldLength);
            txtIdentity.SelectionStart = Math.Max(0, Math.Min(newSelectionStart, newLength));
        }
    }
}
