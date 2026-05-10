namespace PizzaPos.WinForms.UserControls.Admin;

partial class CustomerManagementControl
{
    private System.ComponentModel.IContainer components = null;

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
        this.dgvCustomers = new System.Windows.Forms.DataGridView();
        this.pnlActions = new System.Windows.Forms.Panel();
        this.btnToggleCustomer = new System.Windows.Forms.Button();
        this.btnEditCustomer = new System.Windows.Forms.Button();
        this.btnNewCustomer = new System.Windows.Forms.Button();
        this.lblTitle = new System.Windows.Forms.Label();
        this.txtSearch = new System.Windows.Forms.TextBox();

        ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
        this.pnlActions.SuspendLayout();
        this.SuspendLayout();

        // dgvCustomers
        this.dgvCustomers.AllowUserToAddRows = false;
        this.dgvCustomers.BackgroundColor = System.Drawing.Color.White;
        this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvCustomers.Location = new System.Drawing.Point(0, 80);
        this.dgvCustomers.Name = "dgvCustomers";
        this.dgvCustomers.ReadOnly = true;
        this.dgvCustomers.RowHeadersVisible = false;
        this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvCustomers.Size = new System.Drawing.Size(800, 370);

        // pnlActions
        this.pnlActions.Controls.Add(this.txtSearch);
        this.pnlActions.Controls.Add(this.btnToggleCustomer);
        this.pnlActions.Controls.Add(this.btnEditCustomer);
        this.pnlActions.Controls.Add(this.btnNewCustomer);
        this.pnlActions.Controls.Add(this.lblTitle);
        this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlActions.Location = new System.Drawing.Point(0, 0);
        this.pnlActions.Name = "pnlActions";
        this.pnlActions.Size = new System.Drawing.Size(800, 80);

        // txtSearch
        this.txtSearch.Location = new System.Drawing.Point(20, 45);
        this.txtSearch.Size = new System.Drawing.Size(250, 23);
        this.txtSearch.PlaceholderText = "Buscar por nombre, teléfono...";
        this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

        // btnToggleCustomer
        this.btnToggleCustomer.Location = new System.Drawing.Point(680, 40);
        this.btnToggleCustomer.Name = "btnToggleCustomer";
        this.btnToggleCustomer.Size = new System.Drawing.Size(100, 30);
        this.btnToggleCustomer.Text = "Activar/Desact.";
        this.btnToggleCustomer.UseVisualStyleBackColor = true;
        this.btnToggleCustomer.Click += new System.EventHandler(this.btnToggleCustomer_Click);

        // btnEditCustomer
        this.btnEditCustomer.Location = new System.Drawing.Point(570, 40);
        this.btnEditCustomer.Name = "btnEditCustomer";
        this.btnEditCustomer.Size = new System.Drawing.Size(100, 30);
        this.btnEditCustomer.Text = "Editar";
        this.btnEditCustomer.UseVisualStyleBackColor = true;
        this.btnEditCustomer.Click += new System.EventHandler(this.btnEditCustomer_Click);

        // btnNewCustomer
        this.btnNewCustomer.Location = new System.Drawing.Point(460, 40);
        this.btnNewCustomer.Name = "btnNewCustomer";
        this.btnNewCustomer.Size = new System.Drawing.Size(100, 30);
        this.btnNewCustomer.Text = "Nuevo Cliente";
        this.btnNewCustomer.UseVisualStyleBackColor = true;
        this.btnNewCustomer.Click += new System.EventHandler(this.btnNewCustomer_Click);

        // lblTitle
        this.lblTitle.AutoSize = true;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.lblTitle.Location = new System.Drawing.Point(15, 10);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(250, 30);
        this.lblTitle.Text = "CARTERA DE CLIENTES";

        // CustomerManagementControl
        this.Controls.Add(this.dgvCustomers);
        this.Controls.Add(this.pnlActions);
        this.Name = "CustomerManagementControl";
        this.Size = new System.Drawing.Size(800, 450);
        ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
        this.pnlActions.ResumeLayout(false);
        this.pnlActions.PerformLayout();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.DataGridView dgvCustomers;
    private System.Windows.Forms.Panel pnlActions;
    private System.Windows.Forms.Button btnToggleCustomer;
    private System.Windows.Forms.Button btnEditCustomer;
    private System.Windows.Forms.Button btnNewCustomer;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.TextBox txtSearch;
}
