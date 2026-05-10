namespace PizzaPos.WinForms.UserControls.Common;

public partial class SidebarControl : UserControl
{
    public event EventHandler? NewOrderClick;
    public event EventHandler? ActiveOrdersClick;
    public event EventHandler? HistoryClick;
    public event EventHandler? SettingsClick;
    public event EventHandler? ManageUsersClick;
    public event EventHandler? SecurityClick;

    public SidebarControl(bool isAdmin)
    {
        InitializeComponent();
        btnManageUsers.Visible = isAdmin;
        btnSecurity.Visible = isAdmin;
    }

    private void btnNewOrder_Click(object sender, EventArgs e) => NewOrderClick?.Invoke(this, EventArgs.Empty);
    private void btnActiveOrders_Click(object sender, EventArgs e) => ActiveOrdersClick?.Invoke(this, EventArgs.Empty);
    private void btnHistory_Click(object sender, EventArgs e) => HistoryClick?.Invoke(this, EventArgs.Empty);
    private void btnSettings_Click(object sender, EventArgs e) => SettingsClick?.Invoke(this, EventArgs.Empty);
    private void btnManageUsers_Click(object sender, EventArgs e) => ManageUsersClick?.Invoke(this, EventArgs.Empty);
    private void btnSecurity_Click(object sender, EventArgs e) => SecurityClick?.Invoke(this, EventArgs.Empty);
}
