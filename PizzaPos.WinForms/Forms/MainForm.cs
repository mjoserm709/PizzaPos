using PizzaPos.WinForms.UserControls.Common;
using PizzaPos.WinForms.UserControls.Admin;
using PizzaPos.WinForms.UserControls.Orders;

namespace PizzaPos.WinForms.Forms;

public partial class MainForm : Form
{
    private readonly string _userDisplayName;
    private readonly List<string> _roles;
    private readonly List<string> _permissions;
    private readonly string _token;

    private UserControl? _currentControl;
    private HeaderControl? _header;
    private SidebarControl? _sidebar;

    public bool IsLoggingOut { get; private set; } = false;

    public MainForm(string userDisplayName, List<string> roles, List<string> permissions, string token)
    {
        InitializeComponent();
        _userDisplayName = userDisplayName;
        _roles = roles;
        _permissions = permissions;
        _token = token;
        
        SetupLayout();
    }

    private void SetupLayout()
    {
        bool isAdmin = _roles.Any(r => r.Equals("admin", StringComparison.OrdinalIgnoreCase));

        // Header
        _header = new HeaderControl(_userDisplayName, _roles);
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
        _sidebar.NewOrderClick += (s, e) => LoadControl(new OrderCreationControl(_token));
        _sidebar.ManageUsersClick += (s, e) => LoadControl(new UserManagementControl(_token, _permissions));
        _sidebar.SecurityClick += (s, e) => LoadControl(new SecurityManagementControl(_token, _permissions));
        
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
