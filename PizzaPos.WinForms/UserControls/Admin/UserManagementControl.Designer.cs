namespace PizzaPos.WinForms.UserControls.Admin;

partial class UserManagementControl
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
        this.pnlList = new System.Windows.Forms.Panel();
        this.pnlForm = new System.Windows.Forms.Panel();
        this.lblTitleList = new System.Windows.Forms.Label();
        this.btnNew = new System.Windows.Forms.Button();
        this.btnEdit = new System.Windows.Forms.Button();
        this.dgvUsers = new System.Windows.Forms.DataGridView();
        this.lblTitleForm = new System.Windows.Forms.Label();
        this.lblUsername = new System.Windows.Forms.Label();
        this.txtNewUsername = new System.Windows.Forms.TextBox();
        this.lblPassword = new System.Windows.Forms.Label();
        this.txtNewPassword = new System.Windows.Forms.TextBox();
        this.lblConfirmPassword = new System.Windows.Forms.Label();
        this.txtConfirmPassword = new System.Windows.Forms.TextBox();
        this.lblFullName = new System.Windows.Forms.Label();
        this.txtFullName = new System.Windows.Forms.TextBox();
        this.lblIdentity = new System.Windows.Forms.Label();
        this.txtIdentity = new System.Windows.Forms.TextBox();
        this.chkIsActive = new System.Windows.Forms.CheckBox();
        this.lblRoles = new System.Windows.Forms.Label();
        this.clbRoles = new System.Windows.Forms.CheckedListBox();
        this.lblPermissions = new System.Windows.Forms.Label();
        this.clbPermissions = new System.Windows.Forms.CheckedListBox();
        this.btnSave = new System.Windows.Forms.Button();
        this.btnCancel = new System.Windows.Forms.Button();
        this.btnToggleStatus = new System.Windows.Forms.Button();
        this.txtSearch = new System.Windows.Forms.TextBox();
        this.lblSearch = new System.Windows.Forms.Label();
        
        this.pnlList.SuspendLayout();
        this.pnlForm.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
        this.SuspendLayout();

        // pnlList
        this.pnlList.Controls.Add(this.btnToggleStatus);
        this.pnlList.Controls.Add(this.lblSearch);
        this.pnlList.Controls.Add(this.txtSearch);
        this.pnlList.Controls.Add(this.btnEdit);
        this.pnlList.Controls.Add(this.btnNew);
        this.pnlList.Controls.Add(this.dgvUsers);
        this.pnlList.Controls.Add(this.lblTitleList);
        this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlList.Location = new System.Drawing.Point(0, 0);
        this.pnlList.Name = "pnlList";
        this.pnlList.Size = new System.Drawing.Size(1000, 450);
        this.pnlList.TabIndex = 0;

        // lblTitleList
        this.lblTitleList.AutoSize = true;
        this.lblTitleList.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblTitleList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(90)))), ((int)(((byte)(39)))));
        this.lblTitleList.Location = new System.Drawing.Point(20, 20);
        this.lblTitleList.Text = "Gestión de Usuarios";

        // btnNew
        this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
        this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnNew.ForeColor = System.Drawing.Color.White;
        this.btnNew.Location = new System.Drawing.Point(20, 70);
        this.btnNew.Name = "btnNew";
        this.btnNew.Size = new System.Drawing.Size(120, 35);
        this.btnNew.Text = "+ Nuevo";
        this.btnNew.Click += new System.EventHandler(this.btnNew_Click);

        // btnEdit
        this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
        this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnEdit.ForeColor = System.Drawing.Color.White;
        this.btnEdit.Location = new System.Drawing.Point(150, 70);
        this.btnEdit.Name = "btnEdit";
        this.btnEdit.Size = new System.Drawing.Size(120, 35);
        this.btnEdit.Text = "Editar";
        this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

        // btnToggleStatus
        this.btnToggleStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
        this.btnToggleStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnToggleStatus.ForeColor = System.Drawing.Color.White;
        this.btnToggleStatus.Location = new System.Drawing.Point(280, 70);
        this.btnToggleStatus.Name = "btnToggleStatus";
        this.btnToggleStatus.Size = new System.Drawing.Size(140, 35);
        this.btnToggleStatus.Text = "Activar/Desactivar";
        this.btnToggleStatus.Click += new System.EventHandler(this.btnToggleStatus_Click);

        // lblSearch
        this.lblSearch.AutoSize = true;
        this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(90)))), ((int)(((byte)(39)))));
        this.lblSearch.Location = new System.Drawing.Point(650, 30);
        this.lblSearch.Text = "Buscar:";

        // txtSearch
        this.txtSearch.Location = new System.Drawing.Point(700, 27);
        this.txtSearch.Name = "txtSearch";
        this.txtSearch.Size = new System.Drawing.Size(280, 23);
        this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

        // dgvUsers
        this.dgvUsers.AllowUserToAddRows = false;
        this.dgvUsers.AllowUserToDeleteRows = false;
        this.dgvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
        this.dgvUsers.Location = new System.Drawing.Point(20, 120);
        this.dgvUsers.Name = "dgvUsers";
        this.dgvUsers.ReadOnly = true;
        this.dgvUsers.Size = new System.Drawing.Size(960, 300);

        // pnlForm (Oculto por defecto)
        this.pnlForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
        this.pnlForm.Controls.Add(this.btnCancel);
        this.pnlForm.Controls.Add(this.btnSave);
        this.pnlForm.Controls.Add(this.clbPermissions);
        this.pnlForm.Controls.Add(this.lblPermissions);
        this.pnlForm.Controls.Add(this.clbRoles);
        this.pnlForm.Controls.Add(this.lblRoles);
        this.pnlForm.Controls.Add(this.chkIsActive);
        this.pnlForm.Controls.Add(this.txtIdentity);
        this.pnlForm.Controls.Add(this.lblIdentity);
        this.pnlForm.Controls.Add(this.txtFullName);
        this.pnlForm.Controls.Add(this.lblFullName);
        this.pnlForm.Controls.Add(this.txtConfirmPassword);
        this.pnlForm.Controls.Add(this.lblConfirmPassword);
        this.pnlForm.Controls.Add(this.txtNewPassword);
        this.pnlForm.Controls.Add(this.lblPassword);
        this.pnlForm.Controls.Add(this.txtNewUsername);
        this.pnlForm.Controls.Add(this.lblUsername);
        this.pnlForm.Controls.Add(this.lblTitleForm);
        this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlForm.Location = new System.Drawing.Point(0, 0);
        this.pnlForm.Name = "pnlForm";
        this.pnlForm.Visible = false;
        this.pnlForm.Size = new System.Drawing.Size(1000, 450);

        // Controles de pnlForm (posiciones ajustadas)
        this.lblTitleForm.AutoSize = true;
        this.lblTitleForm.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        this.lblTitleForm.Location = new System.Drawing.Point(30, 20);
        this.lblTitleForm.Text = "Datos del Usuario";

        // Columna 1
        this.lblUsername.Location = new System.Drawing.Point(40, 70);
        this.lblUsername.Text = "Usuario:";
        this.txtNewUsername.Location = new System.Drawing.Point(40, 90);
        this.txtNewUsername.Size = new System.Drawing.Size(200, 23);

        this.lblPassword.Location = new System.Drawing.Point(40, 130);
        this.lblPassword.Text = "Contraseña:";
        this.txtNewPassword.Location = new System.Drawing.Point(40, 150);
        this.txtNewPassword.Size = new System.Drawing.Size(200, 23);
        this.txtNewPassword.PasswordChar = '*';

        this.lblConfirmPassword.Location = new System.Drawing.Point(40, 190);
        this.lblConfirmPassword.Text = "Confirmar Contraseña:";
        this.txtConfirmPassword.Location = new System.Drawing.Point(40, 210);
        this.txtConfirmPassword.Size = new System.Drawing.Size(200, 23);
        this.txtConfirmPassword.PasswordChar = '*';

        // Columna 2
        this.lblFullName.Location = new System.Drawing.Point(280, 70);
        this.lblFullName.Text = "Nombre Completo:";
        this.txtFullName.Location = new System.Drawing.Point(280, 90);
        this.txtFullName.Size = new System.Drawing.Size(200, 23);

        this.lblIdentity.Location = new System.Drawing.Point(280, 130);
        this.lblIdentity.Text = "Identidad:";
        this.txtIdentity.Location = new System.Drawing.Point(280, 150);
        this.txtIdentity.Size = new System.Drawing.Size(200, 23);

        this.chkIsActive.Location = new System.Drawing.Point(280, 200);
        this.chkIsActive.Text = "Usuario Activo";
        this.chkIsActive.Checked = true;

        // Columna 3
        this.lblRoles.Location = new System.Drawing.Point(520, 70);
        this.lblRoles.Text = "Roles:";
        this.clbRoles.Location = new System.Drawing.Point(520, 90);
        this.clbRoles.Size = new System.Drawing.Size(200, 100);

        this.lblPermissions.Location = new System.Drawing.Point(520, 200);
        this.lblPermissions.Text = "Permisos Adicionales:";
        this.clbPermissions.Location = new System.Drawing.Point(520, 220);
        this.clbPermissions.Size = new System.Drawing.Size(200, 100);

        // Botones de acción
        this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
        this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSave.ForeColor = System.Drawing.Color.White;
        this.btnSave.Location = new System.Drawing.Point(40, 350);
        this.btnSave.Size = new System.Drawing.Size(150, 40);
        this.btnSave.Text = "Guardar Usuario";
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

        this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
        this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnCancel.ForeColor = System.Drawing.Color.White;
        this.btnCancel.Location = new System.Drawing.Point(200, 350);
        this.btnCancel.Size = new System.Drawing.Size(120, 40);
        this.btnCancel.Text = "Cancelar";
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

        // UserManagementControl
        this.Controls.Add(this.pnlForm);
        this.Controls.Add(this.pnlList);
        this.Name = "UserManagementControl";
        this.Size = new System.Drawing.Size(1000, 450);
        this.pnlList.ResumeLayout(false);
        this.pnlList.PerformLayout();
        this.pnlForm.ResumeLayout(false);
        this.pnlForm.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Panel pnlList;
    private System.Windows.Forms.Panel pnlForm;
    private System.Windows.Forms.Label lblTitleList;
    private System.Windows.Forms.Button btnNew;
    private System.Windows.Forms.Button btnEdit;
    private System.Windows.Forms.Button btnToggleStatus;
    private System.Windows.Forms.Label lblSearch;
    private System.Windows.Forms.TextBox txtSearch;
    private System.Windows.Forms.DataGridView dgvUsers;
    private System.Windows.Forms.Label lblTitleForm;
    private System.Windows.Forms.Label lblUsername;
    private System.Windows.Forms.TextBox txtNewUsername;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox txtNewPassword;
    private System.Windows.Forms.Label lblConfirmPassword;
    private System.Windows.Forms.TextBox txtConfirmPassword;
    private System.Windows.Forms.Label lblFullName;
    private System.Windows.Forms.TextBox txtFullName;
    private System.Windows.Forms.Label lblIdentity;
    private System.Windows.Forms.TextBox txtIdentity;
    private System.Windows.Forms.CheckBox chkIsActive;
    private System.Windows.Forms.Label lblRoles;
    private System.Windows.Forms.CheckedListBox clbRoles;
    private System.Windows.Forms.Label lblPermissions;
    private System.Windows.Forms.CheckedListBox clbPermissions;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
}
