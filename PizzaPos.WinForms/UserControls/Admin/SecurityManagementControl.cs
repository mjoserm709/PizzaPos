using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using System.ComponentModel;
using PizzaPos.WinForms.Models;
using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.UserControls.Admin;

public partial class SecurityManagementControl : UserControl
{
    private readonly string _token;
    private readonly List<string> _permissions;
    private static readonly HttpClient _httpClient = new HttpClient();
    
    private BindingList<PermissionResponse> _permissionsCatalog = new();
    private BindingList<RoleResponse> _roles = new();
    
    private PermissionResponse? _selectedPermission;
    private RoleResponse? _selectedRole;

    private List<PermissionSelection> _masterRolePermSelection = new();
    private List<PermissionListItem> _allPermissionItems = new();
    
    public SecurityManagementControl(string token, List<string> permissions)
    {
        InitializeComponent();
        _token = token;
        _permissions = permissions;
        
        dgvPermissions.AutoGenerateColumns = true;
        dgvRoles.AutoGenerateColumns = true;

        // Confirmar cambios del checkbox inmediatamente
        dgvRolePermissions.CurrentCellDirtyStateChanged += (s, e) => {
            if (dgvRolePermissions.IsCurrentCellDirty && dgvRolePermissions.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvRolePermissions.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        };
        
        this.Load += async (s, e) => {
            ApplyPermissions();
            pnlPermList.Visible = true;
            pnlPermList.BringToFront();
            pnlRoleList.Visible = true;
            pnlRoleList.BringToFront();
            await RefreshAll();
        };
    }

    private void ApplyPermissions()
    {
        bool canManageRoles = _permissions.Contains("roles.manage");
        bool canManagePerms = _permissions.Contains("permissions.manage");

        // Configuración de Roles
        tabRoles.Enabled = canManageRoles;
        btnNewRole.Visible = canManageRoles;
        btnEditRole.Visible = canManageRoles;
        btnToggleRole.Visible = canManageRoles;

        // Configuración de Permisos
        tabPermissions.Enabled = canManagePerms;
        btnNewPerm.Visible = canManagePerms;
        btnEditPerm.Visible = canManagePerms;
        btnTogglePerm.Visible = canManagePerms;
    }

    private async Task RefreshAll()
    {
        await LoadPermissions();
        await LoadRoles();
    }

    private async Task LoadPermissions()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await _httpClient.GetAsync("http://localhost:5267/api/security/permissions");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<PermissionResponse>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result != null && result.Success && result.Data != null)
                {
                    _permissionsCatalog = new BindingList<PermissionResponse>(result.Data);
                    UpdatePermissionsGrid();
                    
                    _allPermissionItems.Clear();
                    foreach (var p in result.Data.Where(x => x.IsActive))
                    {
                        _allPermissionItems.Add(new PermissionListItem(p.Id, p.Name, p.Description));
                    }
                }
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error cargando permisos: " + ex.Message); }
    }

    private void UpdatePermissionsGrid()
    {
        dgvPermissions.DataSource = null;
        var display = _permissionsCatalog.Select(p => new {
            p.Id,
            Permiso = p.Name,
            Descripción = p.Description,
            Activo = p.IsActive ? "Sí" : "No",
            Creado = p.CreatedAt.ToString("g"),
            Por = p.CreatedBy
        }).ToList();
        dgvPermissions.DataSource = new BindingList<object>(display.Cast<object>().ToList());
        if (dgvPermissions.Columns.Count > 0) dgvPermissions.Columns["Id"].Visible = false;
    }

    private async Task LoadRoles()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await _httpClient.GetAsync("http://localhost:5267/api/security/roles");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<RoleResponse>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result != null && result.Success && result.Data != null)
                {
                    _roles = new BindingList<RoleResponse>(result.Data);
                    UpdateRolesGrid();
                }
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error cargando roles: " + ex.Message); }
    }

    private void UpdateRolesGrid()
    {
        dgvRoles.DataSource = null;
        var display = _roles.Select(r => new {
            r.Id,
            Rol = r.Name,
            Activo = r.IsActive ? "Sí" : "No",
            Permisos = string.Join(", ", r.Permissions),
            Creado = r.CreatedAt.ToString("g"),
            Por = r.CreatedBy
        }).ToList();
        dgvRoles.DataSource = new BindingList<object>(display.Cast<object>().ToList());
        if (dgvRoles.Columns.Count > 0) dgvRoles.Columns["Id"].Visible = false;
    }

    private void btnNewPerm_Click(object sender, EventArgs e)
    {
        _selectedPermission = null;
        txtPermName.Clear();
        txtPermDesc.Clear();
        pnlPermForm.Visible = true;
        pnlPermForm.BringToFront();
        pnlPermList.Visible = false;
    }

    private void btnEditPerm_Click(object sender, EventArgs e)
    {
        if (dgvPermissions.CurrentRow == null || dgvPermissions.CurrentRow.Cells["Id"].Value == null) 
        {
            ToastNotification.Info("Por favor, seleccione un permiso de la lista.");
            return;
        }
        
        var id = (int)dgvPermissions.CurrentRow.Cells["Id"].Value;
        _selectedPermission = _permissionsCatalog.FirstOrDefault(p => p.Id == id);
        
        if (_selectedPermission != null)
        {
            txtPermName.Text = _selectedPermission.Name;
            txtPermDesc.Text = _selectedPermission.Description;
            pnlPermForm.Visible = true;
            pnlPermForm.BringToFront();
            pnlPermList.Visible = false;
        }
    }

    private async void btnSavePerm_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtPermName.Text)) 
        {
            ToastNotification.Warning("El nombre del permiso es obligatorio.");
            return;
        }

        object data;
        string url = "http://localhost:5267/api/security/permissions";
        HttpMethod method;

        if (_selectedPermission == null)
        {
            data = new CreatePermissionRequest(txtPermName.Text, txtPermDesc.Text);
            method = HttpMethod.Post;
        }
        else
        {
            data = new UpdatePermissionRequest(_selectedPermission.Id, txtPermName.Text, txtPermDesc.Text);
            method = HttpMethod.Put;
        }

        try
        {
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            var request = new HttpRequestMessage(method, url) { Content = content };
            var res = await _httpClient.SendAsync(request);
            var jsonRes = await res.Content.ReadAsStringAsync();
            var dynamicRes = JsonSerializer.Deserialize<DynamicResponse<string>>(jsonRes, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (res.IsSuccessStatusCode && dynamicRes != null && dynamicRes.Success)
            {
                ToastNotification.Success(dynamicRes.Message ?? "Permiso guardado correctamente");
                pnlPermForm.Visible = false;
                pnlPermList.Visible = true;
                await LoadPermissions();
            }
            else 
            {
                string error = dynamicRes?.Message ?? "Error al guardar permiso";
                if (dynamicRes?.Errors != null && dynamicRes.Errors.Any()) error += ": " + string.Join(", ", dynamicRes.Errors);
                ToastNotification.Error(error);
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error de conexión: " + ex.Message); }
    }

    private void btnCancelPerm_Click(object sender, EventArgs e)
    {
        pnlPermForm.Visible = false;
        pnlPermList.Visible = true;
    }

    private async void btnTogglePerm_Click(object sender, EventArgs e)
    {
        if (dgvPermissions.CurrentRow == null) return;
        var id = (int)dgvPermissions.CurrentRow.Cells["Id"].Value;
        var perm = _permissionsCatalog.FirstOrDefault(p => p.Id == id);
        if (perm == null) return;

        var data = new UpdatePermissionStatusRequest(id, !perm.IsActive);
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        
        var res = await _httpClient.PatchAsync("http://localhost:5267/api/security/permissions/status", content);
        if (res.IsSuccessStatusCode) await LoadPermissions();
    }

    private void btnNewRole_Click(object sender, EventArgs e)
    {
        _selectedRole = null;
        txtRoleName.Clear();
        txtSearchRolePerms.Clear();
        PreparePermissionSelection(new List<int>());
        pnlRoleForm.Visible = true;
        pnlRoleForm.BringToFront();
        pnlRoleList.Visible = false;
    }

    private void btnEditRole_Click(object sender, EventArgs e)
    {
        if (dgvRoles.CurrentRow == null || dgvRoles.CurrentRow.Cells["Id"].Value == null)
        {
            ToastNotification.Info("Por favor, seleccione un rol de la lista.");
            return;
        }

        var id = (int)dgvRoles.CurrentRow.Cells["Id"].Value;
        _selectedRole = _roles.FirstOrDefault(r => r.Id == id);
        
        if (_selectedRole != null)
        {
            txtRoleName.Text = _selectedRole.Name;
            txtSearchRolePerms.Clear();
            
            // Diagnóstico preciso: Ver cuántos permisos dice el servidor que tiene
            int count = _selectedRole.PermissionIds?.Count ?? 0;
            ToastNotification.Info($"Cargando rol con {count} permisos asignados (Validación por ID).");

            PreparePermissionSelection(_selectedRole.PermissionIds ?? new List<int>());
            
            pnlRoleForm.Visible = true;
            pnlRoleForm.BringToFront();
            pnlRoleList.Visible = false;
        }
    }

    private void PreparePermissionSelection(List<int> selectedIds)
    {
        _masterRolePermSelection.Clear();
        foreach (var p in _allPermissionItems)
        {
            // COMPARACIÓN POR ID: Infalible y profesional
            bool isSelected = selectedIds.Contains(p.Id);
            _masterRolePermSelection.Add(new PermissionSelection {
                Selected = isSelected,
                Name = p.Name,
                Description = p.Description,
                Id = p.Id // Aseguramos que el modelo de selección también tenga el ID
            });
        }
        
        RefreshRolePermissionsGrid();
    }

    private void RefreshRolePermissionsGrid(string filter = "")
    {
        var filtered = _masterRolePermSelection
            .Where(p => string.IsNullOrEmpty(filter) || 
                       p.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) || 
                       p.Description.Contains(filter, StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        dgvRolePermissions.DataSource = null;
        dgvRolePermissions.DataSource = new BindingList<PermissionSelection>(filtered);
        
        if (dgvRolePermissions.Columns.Count > 0)
        {
            dgvRolePermissions.Columns["Selected"].HeaderText = "✓";
            dgvRolePermissions.Columns["Selected"].Width = 40;
            dgvRolePermissions.Columns["Name"].ReadOnly = true;
            dgvRolePermissions.Columns["Description"].ReadOnly = true;
            dgvRolePermissions.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }

    private void txtSearchRolePerms_TextChanged(object sender, EventArgs e)
    {
        RefreshRolePermissionsGrid(txtSearchRolePerms.Text);
    }

    private async void btnSaveRole_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtRoleName.Text)) 
        {
            ToastNotification.Warning("El nombre del rol es obligatorio.");
            return;
        }

        var perms = _masterRolePermSelection.Where(x => x.Selected).Select(x => x.Id).ToList();
        object data;
        string url = "http://localhost:5267/api/security/roles";
        HttpMethod method;

        if (_selectedRole == null)
        {
            data = new CreateRoleRequest(txtRoleName.Text, perms);
            method = HttpMethod.Post;
        }
        else
        {
            data = new UpdateRoleRequest(_selectedRole.Id, txtRoleName.Text, perms);
            method = HttpMethod.Put;
        }

        try
        {
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            var request = new HttpRequestMessage(method, url) { Content = content };
            var res = await _httpClient.SendAsync(request);
            var jsonRes = await res.Content.ReadAsStringAsync();
            var dynamicRes = JsonSerializer.Deserialize<DynamicResponse<string>>(jsonRes, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (res.IsSuccessStatusCode && dynamicRes != null && dynamicRes.Success)
            {
                ToastNotification.Success(dynamicRes.Message ?? "Rol guardado correctamente");
                pnlRoleForm.Visible = false;
                pnlRoleList.Visible = true;
                await LoadRoles();
            }
            else 
            {
                string error = dynamicRes?.Message ?? "Error al guardar rol";
                if (dynamicRes?.Errors != null && dynamicRes.Errors.Any()) error += ": " + string.Join(", ", dynamicRes.Errors);
                ToastNotification.Error(error);
            }
        }
        catch (Exception ex) { ToastNotification.Error("Error de conexión: " + ex.Message); }
    }

    private void btnCancelRole_Click(object sender, EventArgs e)
    {
        pnlRoleForm.Visible = false;
        pnlRoleList.Visible = true;
    }

    private async void btnToggleRole_Click(object sender, EventArgs e)
    {
        if (dgvRoles.CurrentRow == null) return;
        var id = (int)dgvRoles.CurrentRow.Cells["Id"].Value;
        var role = _roles.FirstOrDefault(r => r.Id == id);
        if (role == null) return;

        var data = new UpdateRoleStatusRequest(id, !role.IsActive);
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        
        var res = await _httpClient.PatchAsync("http://localhost:5267/api/security/roles/status", content);
        if (res.IsSuccessStatusCode) await LoadRoles();
    }

}

public record PermissionListItem(int Id, string Name, string Description);

public class PermissionSelection
{
    public bool Selected { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
