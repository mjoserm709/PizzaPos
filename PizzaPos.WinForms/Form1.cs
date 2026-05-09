namespace PizzaPos.WinForms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private async void btnCheckStatus_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Checking status...";
        using var client = new HttpClient();
        try
        {
            // Note: Update port if necessary based on API launch settings
            var response = await client.GetStringAsync("http://localhost:5267/api/status");
            lblStatus.Text = $"Status: {response}";
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Error: {ex.Message}";
        }
    }
}
