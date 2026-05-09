using PizzaPos.WinForms.Utils;

namespace PizzaPos.WinForms.UserControls.Common;

public partial class HeaderControl : UserControl
{
    public event EventHandler LogoutClick;

    public HeaderControl(string username, List<string> roles)
    {
        InitializeComponent();
        lblSystemName.Text = "PIZZA POS - Sistema de Ventas";
        lblUserInfo.Text = $"{username} | {string.Join(", ", roles)}";
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        LogoutClick?.Invoke(this, EventArgs.Empty);
    }
}
