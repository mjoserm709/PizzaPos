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
            var response = await _httpClient.GetAsync("http://localhost:5267/api/users/permissions");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DynamicResponse<List<string>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result != null && result.Success && result.Data != null)
                {
                    clbRolePermissions.Items.Clear();
                    foreach (var perm in result.Data) clbRolePermissions.Items.Add(perm);
                }
            }
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private async void btnCreatePermission_Click(object sender, EventArgs e)
    {
        var data = new { name = txtPermName.Text, description = txtPermDesc.Text };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var res = await _httpClient.PostAsync("http://localhost:5267/api/security/permissions", content);
        if (res.IsSuccessStatusCode) 
        { 
            var json = await res.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DynamicResponse<string>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            MessageBox.Show(result?.Message ?? "Permiso creado"); 
            await LoadPermissions(); 
        }
        else
        {
            var json = await res.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DynamicResponse<string>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            string error = result?.Message ?? "Error al crear permiso";
            if (result?.Errors != null && result.Errors.Any()) error += "\n" + string.Join("\n", result.Errors);
            MessageBox.Show(error);
        }
    }

    private async void btnCreateRole_Click(object sender, EventArgs e)
    {
        var perms = clbRolePermissions.CheckedItems.Cast<string>().ToList();
        var data = new { name = txtRoleName.Text, permissionNames = perms };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var res = await _httpClient.PostAsync("http://localhost:5267/api/security/roles", content);
        if (res.IsSuccessStatusCode) 
        { 
            var json = await res.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DynamicResponse<string>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            MessageBox.Show(result?.Message ?? "Rol creado"); 
        }
        else
        {
            var json = await res.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DynamicResponse<string>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            string error = result?.Message ?? "Error al crear rol";
            if (result?.Errors != null && result.Errors.Any()) error += "\n" + string.Join("\n", result.Errors);
            MessageBox.Show(error);
        }
    }
}
