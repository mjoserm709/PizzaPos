namespace PizzaPos.WinForms;

public partial class MainForm : Form
{
    private readonly string _username;
    private readonly List<string> _roles;
    private readonly string _token;

    private Form? _currentForm;

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

    private void LoadFormIntoPanel(Form childForm)
    {
        if (_currentForm != null)
        {
            _currentForm.Close();
        }

        _currentForm = childForm;
        childForm.TopLevel = false;
        childForm.FormBorderStyle = FormBorderStyle.None;
        childForm.Dock = DockStyle.Fill;
        
        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(childForm);
        pnlContent.Tag = childForm;
        childForm.Show();
    }

    private void btnManageUsers_Click(object sender, EventArgs e)
    {
        LoadFormIntoPanel(new UserManagementForm(_token));
    }

    private void btnSecurity_Click(object sender, EventArgs e)
    {
        LoadFormIntoPanel(new SecurityManagementForm(_token));
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        Application.Exit();
    }
}
