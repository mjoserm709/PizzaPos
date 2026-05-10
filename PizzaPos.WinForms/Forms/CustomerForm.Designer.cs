namespace PizzaPos.WinForms.Forms;

partial class CustomerForm
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
        this.lblFullName = new System.Windows.Forms.Label();
        this.txtFullName = new System.Windows.Forms.TextBox();
        this.lblPhone = new System.Windows.Forms.Label();
        this.txtPhone = new System.Windows.Forms.TextBox();
        this.lblEmail = new System.Windows.Forms.Label();
        this.txtEmail = new System.Windows.Forms.TextBox();
        this.grpAddresses = new System.Windows.Forms.GroupBox();
        this.dgvAddresses = new System.Windows.Forms.DataGridView();
        this.btnAddAddress = new System.Windows.Forms.Button();
        this.btnRemoveAddress = new System.Windows.Forms.Button();
        this.btnSave = new System.Windows.Forms.Button();
        this.btnCancel = new System.Windows.Forms.Button();
        this.grpAddresses.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvAddresses)).BeginInit();
        this.SuspendLayout();

        // lblFullName
        this.lblFullName.Location = new System.Drawing.Point(20, 10);
        this.lblFullName.Size = new System.Drawing.Size(120, 20);
        this.lblFullName.Text = "Nombre Completo:";

        // txtFullName
        this.txtFullName.Location = new System.Drawing.Point(20, 30);
        this.txtFullName.Size = new System.Drawing.Size(460, 23);

        // lblPhone
        this.lblPhone.Location = new System.Drawing.Point(20, 60);
        this.lblPhone.Size = new System.Drawing.Size(100, 20);
        this.lblPhone.Text = "Teléfono:";

        // txtPhone
        this.txtPhone.Location = new System.Drawing.Point(20, 80);
        this.txtPhone.Size = new System.Drawing.Size(220, 23);

        // lblEmail
        this.lblEmail.Location = new System.Drawing.Point(260, 60);
        this.lblEmail.Size = new System.Drawing.Size(100, 20);
        this.lblEmail.Text = "Email:";

        // txtEmail
        this.txtEmail.Location = new System.Drawing.Point(260, 80);
        this.txtEmail.Size = new System.Drawing.Size(220, 23);

        // grpAddresses
        this.grpAddresses.Controls.Add(this.dgvAddresses);
        this.grpAddresses.Controls.Add(this.btnAddAddress);
        this.grpAddresses.Controls.Add(this.btnRemoveAddress);
        this.grpAddresses.Location = new System.Drawing.Point(20, 120);
        this.grpAddresses.Name = "grpAddresses";
        this.grpAddresses.Size = new System.Drawing.Size(460, 200);
        this.grpAddresses.TabIndex = 6;
        this.grpAddresses.TabStop = false;
        this.grpAddresses.Text = "Direcciones de Entrega";

        // dgvAddresses
        this.dgvAddresses.AllowUserToAddRows = true; // Permitir edición directa
        this.dgvAddresses.BackgroundColor = System.Drawing.Color.White;
        this.dgvAddresses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvAddresses.Location = new System.Drawing.Point(10, 25);
        this.dgvAddresses.Name = "dgvAddresses";
        this.dgvAddresses.RowHeadersVisible = false;
        this.dgvAddresses.Size = new System.Drawing.Size(440, 130);

        // btnAddAddress
        this.btnAddAddress.Location = new System.Drawing.Point(10, 165);
        this.btnAddAddress.Size = new System.Drawing.Size(120, 25);
        this.btnAddAddress.Text = "+ Nueva Línea";
        this.btnAddAddress.Click += (s, e) => { /* Logic in .cs */ };

        // btnRemoveAddress
        this.btnRemoveAddress.Location = new System.Drawing.Point(330, 165);
        this.btnRemoveAddress.Size = new System.Drawing.Size(120, 25);
        this.btnRemoveAddress.Text = "- Quitar Selecc.";
        this.btnRemoveAddress.Click += (s, e) => { /* Logic in .cs */ };

        // btnSave
        this.btnSave.BackColor = System.Drawing.Color.FromArgb(26, 51, 23);
        this.btnSave.ForeColor = System.Drawing.Color.White;
        this.btnSave.Location = new System.Drawing.Point(270, 335);
        this.btnSave.Size = new System.Drawing.Size(100, 35);
        this.btnSave.Text = "Guardar";
        this.btnSave.UseVisualStyleBackColor = false;
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

        // btnCancel
        this.btnCancel.Location = new System.Drawing.Point(380, 335);
        this.btnCancel.Size = new System.Drawing.Size(100, 35);
        this.btnCancel.Text = "Cancelar";
        this.btnCancel.Click += (s, e) => this.Close();

        // CustomerForm
        this.ClientSize = new System.Drawing.Size(500, 385);
        this.Controls.Add(this.grpAddresses);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.txtEmail);
        this.Controls.Add(this.lblEmail);
        this.Controls.Add(this.txtPhone);
        this.Controls.Add(this.lblPhone);
        this.Controls.Add(this.txtFullName);
        this.Controls.Add(this.lblFullName);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.grpAddresses.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dgvAddresses)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label lblFullName;
    private System.Windows.Forms.TextBox txtFullName;
    private System.Windows.Forms.Label lblPhone;
    private System.Windows.Forms.TextBox txtPhone;
    private System.Windows.Forms.Label lblEmail;
    private System.Windows.Forms.TextBox txtEmail;
    private System.Windows.Forms.GroupBox grpAddresses;
    private System.Windows.Forms.DataGridView dgvAddresses;
    private System.Windows.Forms.Button btnAddAddress;
    private System.Windows.Forms.Button btnRemoveAddress;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
}
