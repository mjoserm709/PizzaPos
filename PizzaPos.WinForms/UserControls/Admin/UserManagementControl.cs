using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;
using System.ComponentModel;

namespace PizzaPos.WinForms.UserControls.Admin;

public partial class UserManagementControl : UserControl
{
    private readonly string _token;
    private readonly List<string> _permissions;
    private static readonly HttpClient _httpClient = new HttpClient();
    private int? _selectedUserId;
    private List<UserResponse> _usersList = new();
    private BindingSource _bindingSource = new BindingSource();

    public UserManagementControl(string token, List<string> permissions)
    {
        InitializeComponent();
        _token = token;
        _permissions = permissions;
        txtIdentity.TextChanged += txtIdentity_TextChanged;
        
        dgvUsers.AutoGenerateColumns = true;
        dgvUsers.DataSource = _bindingSource;

        this.Load += async (s, e) => {
            ApplyPermissions();
            await LoadRolesAndPermissions();
            await LoadUsers();
        };
    }

    private void ApplyPermissions()
    {
        btnNew.Visible = _permissions.Contains("users.create");
        btnEdit.Visible = _permissions.Contains("users.edit");
        btnToggleStatus.Visible = _permissions.Contains("users.delete");
    }

    private async Task LoadRolesAndPermissions()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            // Cargar Roles
            var resRoles = await _httpClient.GetAsync("http://localhost:5267/api/users/roles");
            if (resRoles.IsSuccessStatusCode)
            {
                var json = await resRoles.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<string>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result != null && result.Success && result.Data != null)
                {
                    clbRoles.Items.Clear();
                    foreach (var role in result.Data) clbRoles.Items.Add(role);
                }
            }

            // Cargar Permisos
            var resPerms = await _httpClient.GetAsync("http://localhost:5267/api/users/permissions");
            if (resPerms.IsSuccessStatusCode)
            {
                var json = await resPerms.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<string>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result != null && result.Success && result.Data != null)
                {
                    clbPermissions.Items.Clear();
                    foreach (var perm in result.Data) clbPermissions.Items.Add(perm);
                }
            }
            
            if (!resRoles.IsSuccessStatusCode || !resPerms.IsSuccessStatusCode)
            {
                ToastNotification.Error("No se pudieron cargar los catálogos. Verifique sus permisos de administrador.");
            }
        }
        catch (Exception ex) { ToastNotification.Error($"Error cargando catálogos: {ex.Message}"); }
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
                    UpdateGrid();
                }
            }
        }
        catch (Exception ex) { ToastNotification.Error($"Error: {ex.Message}"); }
    }

    private void UpdateGrid(string filter = "")
    {
        var displayList = _usersList
            .Where(u => string.IsNullOrEmpty(filter) || 
                       u.Username.Contains(filter, StringComparison.OrdinalIgnoreCase) || 
                       u.FullName.Contains(filter, StringComparison.OrdinalIgnoreCase))
            .Select(u => new {
                u.Id,
                Usuario = u.Username,
                Nombre = u.FullName,
                Identidad = u.IdentityNumber,
                Activo = u.IsActive ? "Sí" : "No",
                Creado = u.CreatedAt.ToString("g"),
                Por = u.CreatedBy,
                Modificado = u.UpdatedAt?.ToString("g") ?? "-",
                ModPor = u.UpdatedBy ?? "-",
                Roles = string.Join(", ", u.Roles)
            }).ToList();

        // Forzar limpieza para evitar que el grid mantenga datos viejos
        _bindingSource.DataSource = null;
        _bindingSource.DataSource = new BindingList<object>(displayList.Cast<object>().ToList());
        dgvUsers.DataSource = _bindingSource;
        
        if (dgvUsers.Columns.Count > 0)
        {
            dgvUsers.Columns["Id"].Visible = false;
            dgvUsers.Columns["Roles"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        _bindingSource.ResetBindings(false);
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        UpdateGrid(txtSearch.Text);
    }

    private async void btnToggleStatus_Click(object sender, EventArgs e)
    {
        if (dgvUsers.CurrentRow != null)
        {
            var id = (int)dgvUsers.CurrentRow.Cells["Id"].Value;
            var user = _usersList.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                bool newStatus = !user.IsActive;
                string statusText = newStatus ? "activar" : "desactivar";
                
                var statusData = new
                {
                    userId = user.Id,
                    isActive = newStatus
                };

                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    var content = new StringContent(JsonSerializer.Serialize(statusData), Encoding.UTF8, "application/json");
                    
                    // El API usa PATCH para el cambio de estado específico
                    var response = await _httpClient.PatchAsync("http://localhost:5267/api/users/status", content);

                    if (response.IsSuccessStatusCode)
                    {
                        ToastNotification.Success($"Usuario '{user.Username}' {(newStatus ? "activado" : "desactivado")} correctamente.");
                        await LoadUsers();
                    }
                    else 
                    { 
                        var errorJson = await response.Content.ReadAsStringAsync();
                        ToastNotification.Error($"Error del servidor: {response.StatusCode}"); 
                    }
                }
                catch (Exception ex) { ToastNotification.Error($"Error de conexión: {ex.Message}"); }
            }
        }
        else { ToastNotification.Info("Seleccione un usuario de la lista."); }
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        lblTitleForm.Text = "Crear Nuevo Usuario";
        btnSave.Text = "Guardar Usuario";
        pnlList.Visible = false;
        pnlForm.Visible = true;
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (dgvUsers.CurrentRow != null)
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

                lblTitleForm.Text = "Editar Usuario";
                btnSave.Text = "Actualizar Usuario";
                pnlList.Visible = false;
                pnlForm.Visible = true;
            }
        }
        else { ToastNotification.Info("Seleccione un usuario para editar."); }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        pnlForm.Visible = false;
        pnlList.Visible = true;
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

        if (string.IsNullOrEmpty(userData.username) || string.IsNullOrEmpty(userData.fullName) || 
            string.IsNullOrEmpty(userData.identityNumber) || (_selectedUserId == null && string.IsNullOrEmpty(userData.password)))
        {
            ToastNotification.Error("Complete todos los campos requeridos.");
            return;
        }

        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var content = new StringContent(JsonSerializer.Serialize(userData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = _selectedUserId == null 
                ? await _httpClient.PostAsync("http://localhost:5267/api/users", content)
                : await _httpClient.PutAsync("http://localhost:5267/api/users", content);

            if (response.IsSuccessStatusCode)
            {
                ToastNotification.Success($"Usuario '{userData.username}' guardado correctamente.");
                pnlForm.Visible = false;
                pnlList.Visible = true;
                await LoadUsers();
            }
            else
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dynamicResult = JsonSerializer.Deserialize<DynamicResponse<string>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ToastNotification.Error(dynamicResult?.Message ?? "Error en la operación");
            }
        }
        catch (Exception ex) { ToastNotification.Error($"Error: {ex.Message}"); }
    }

    private void txtIdentity_TextChanged(object? sender, EventArgs e)
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
            int newLength = formatted.Length;
            int newSelectionStart = selectionStart + (newLength - oldLength);
            txtIdentity.SelectionStart = Math.Max(0, Math.Min(newSelectionStart, newLength));
        }
    }

    public record UserResponse(int Id, string Username, string FullName, string IdentityNumber, bool IsActive, DateTime CreatedAt, string CreatedBy, DateTime? UpdatedAt, string? UpdatedBy, List<string> Roles, List<string> Permissions);
}
