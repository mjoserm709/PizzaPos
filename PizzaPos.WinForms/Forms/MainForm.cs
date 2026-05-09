using PizzaPos.WinForms.UserControls.Common;
using PizzaPos.WinForms.UserControls.Admin;

namespace PizzaPos.WinForms.Forms;

public partial class MainForm : Form
{
    private readonly string _username;
    private readonly List<string> _roles;
    private readonly string _token;

    private UserControl? _currentControl;
    private HeaderControl _header;
    private SidebarControl _sidebar;

    public bool IsLoggingOut { get; private set; } = false;

    public MainForm(string username, List<string> roles, string token)
    {
        InitializeComponent();
        _username = username;
        _roles = roles;
        _token = token;
        
        SetupLayout();
    }

    private void SetupLayout()
    {
        bool isAdmin = _roles.Contains("Admin");

        // Header
        _header = new HeaderControl(_username, _roles);
        _header.Dock = DockStyle.Top;
        _header.LogoutClick += (s, e) => {
            IsLoggingOut = true;
            this.Close();
        };
        this.Controls.Add(_header);

        // Sidebar
        _sidebar = new SidebarControl(isAdmin);
        _sidebar.Dock = DockStyle.Left;
        _sidebar.Width = 200;
        
        // Eventos del Sidebar
        _sidebar.ManageUsersClick += (s, e) => LoadControl(new UserManagementControl(_token));
        _sidebar.SecurityClick += (s, e) => LoadControl(new SecurityManagementControl(_token));
        
        this.Controls.Add(_sidebar);

        // Panel de Contenido
        pnlContent.Dock = DockStyle.Fill;
        pnlContent.BringToFront();
    }

    private void LoadControl(UserControl control)
    {
        if (_currentControl != null)
        {
            _currentControl.Dispose();
        }

        _currentControl = control;
        control.Dock = DockStyle.Fill;
        
        pnlContent.Controls.Clear();
        pnlContent.Controls.Add(control);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        // El control de si se sale de la app o se vuelve al login lo lleva LoginForm
    }
}
