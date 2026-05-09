namespace PizzaPos.WinForms;

public partial class MainForm : Form
{
    private readonly string _username;
    private readonly List<string> _roles;
    private readonly string _token;

    public MainForm(string username, List<string> roles, string token)
    {
        InitializeComponent();
        _username = username;
        _roles = roles;
        _token = token;
        
        lblWelcome.Text = $"¡Bienvenido, {_username}!";
        lblRole.Text = $"Roles: {string.Join(", ", _roles)}";

        // Solo mostrar botones de gestión si es Admin
        var isAdmin = _roles.Contains("Admin");
        btnManageUsers.Visible = isAdmin;
        btnSecurity.Visible = isAdmin;
    }

    private void btnManageUsers_Click(object sender, EventArgs e)
    {
        var manageForm = new UserManagementForm(_token);
        manageForm.ShowDialog();
    }

    private void btnSecurity_Click(object sender, EventArgs e)
    {
        var securityForm = new SecurityManagementForm(_token);
        securityForm.ShowDialog();
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}
