namespace PizzaPos.WinForms.UserControls.Orders;

partial class OrderManagementControl
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.DataGridView dgvOrders;
    private System.Windows.Forms.Label lblTitle;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.dgvOrders = new System.Windows.Forms.DataGridView();
        this.lblTitle = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
        this.SuspendLayout();

        // dgvOrders
        this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvOrders.Location = new System.Drawing.Point(0, 40);
        this.dgvOrders.Name = "dgvOrders";
        this.dgvOrders.Size = new System.Drawing.Size(800, 560);
        this.dgvOrders.TabIndex = 0;
        this.dgvOrders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrders_CellContentClick);
        this.dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvOrders.AllowUserToAddRows = false;

        // lblTitle
        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.lblTitle.Location = new System.Drawing.Point(0, 0);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(800, 40);
        this.lblTitle.Text = "GESTIÓN DE PEDIDOS ACTIVOS (REACTIVO)";

        // OrderManagementControl
        this.Controls.Add(this.dgvOrders);
        this.Controls.Add(this.lblTitle);
        this.Name = "OrderManagementControl";
        this.Size = new System.Drawing.Size(800, 600);
        ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
        this.ResumeLayout(false);
    }
}
