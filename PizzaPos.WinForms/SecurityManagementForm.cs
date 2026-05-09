using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace PizzaPos.WinForms;

public partial class SecurityManagementForm : Form
{
    private readonly string _token;
    private static readonly HttpClient _httpClient = new HttpClient();

    public SecurityManagementForm(string token)
    {
        InitializeComponent();
        _token = token;
    }

    private async void SecurityManagementForm_Load(object sender, EventArgs e)
    {
        await LoadPermissions();
    }

    private async Task LoadPermissions()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var permsJson = await _httpClient.GetStringAsync("http://localhost:5267/api/users/permissions");
            var perms = JsonSerializer.Deserialize<List<string>>(permsJson);
            clbRolePermissions.Items.Clear();
            if (perms != null) foreach (var perm in perms) clbRolePermissions.Items.Add(perm);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private async void btnCreatePermission_Click(object sender, EventArgs e)
    {
        var data = new { name = txtPermName.Text, description = txtPermDesc.Text };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var res = await _httpClient.PostAsync("http://localhost:5267/api/security/permissions", content);
        if (res.IsSuccessStatusCode) { MessageBox.Show("Permiso creado"); await LoadPermissions(); }
    }

    private async void btnCreateRole_Click(object sender, EventArgs e)
    {
        var perms = clbRolePermissions.CheckedItems.Cast<string>().ToList();
        var data = new { name = txtRoleName.Text, permissionNames = perms };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var res = await _httpClient.PostAsync("http://localhost:5267/api/security/roles", content);
        if (res.IsSuccessStatusCode) { MessageBox.Show("Rol creado"); }
    }
}
