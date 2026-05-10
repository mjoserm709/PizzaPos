namespace PizzaPos.WinForms.UserControls.Admin;

partial class SecurityManagementControl
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
        this.tabControl = new System.Windows.Forms.TabControl();
        this.tabPermissions = new System.Windows.Forms.TabPage();
        this.tabRoles = new System.Windows.Forms.TabPage();
        
        // Permisos
        this.pnlPermList = new System.Windows.Forms.Panel();
        this.pnlPermForm = new System.Windows.Forms.Panel();
        this.dgvPermissions = new System.Windows.Forms.DataGridView();
        this.btnNewPerm = new System.Windows.Forms.Button();
        this.btnEditPerm = new System.Windows.Forms.Button();
        this.btnTogglePerm = new System.Windows.Forms.Button();
        this.lblTitlePerm = new System.Windows.Forms.Label();
        this.txtPermName = new System.Windows.Forms.TextBox();
        this.txtPermDesc = new System.Windows.Forms.TextBox();
        this.btnSavePerm = new System.Windows.Forms.Button();
        this.btnCancelPerm = new System.Windows.Forms.Button();

        // Roles
        this.pnlRoleList = new System.Windows.Forms.Panel();
        this.pnlRoleForm = new System.Windows.Forms.Panel();
        this.dgvRoles = new System.Windows.Forms.DataGridView();
        this.btnNewRole = new System.Windows.Forms.Button();
        this.btnEditRole = new System.Windows.Forms.Button();
        this.btnToggleRole = new System.Windows.Forms.Button();
        this.lblTitleRole = new System.Windows.Forms.Label();
        this.txtRoleName = new System.Windows.Forms.TextBox();
        this.txtSearchRolePerms = new System.Windows.Forms.TextBox();
        this.lblSearchRolePerms = new System.Windows.Forms.Label();
        this.dgvRolePermissions = new System.Windows.Forms.DataGridView();
        this.btnSaveRole = new System.Windows.Forms.Button();
        this.btnCancelRole = new System.Windows.Forms.Button();

        this.tabControl.SuspendLayout();
        this.tabPermissions.SuspendLayout();
        this.tabRoles.SuspendLayout();
        this.pnlPermList.SuspendLayout();
        this.pnlPermForm.SuspendLayout();
        this.pnlRoleList.SuspendLayout();
        this.pnlRoleForm.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvPermissions)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvRolePermissions)).BeginInit();
        this.SuspendLayout();

        // tabControl
        this.tabControl.Controls.Add(this.tabPermissions);
        this.tabControl.Controls.Add(this.tabRoles);
        this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabControl.Location = new System.Drawing.Point(0, 0);
        this.tabControl.Name = "tabControl";
        this.tabControl.SelectedIndex = 0;
        this.tabControl.Size = new System.Drawing.Size(1000, 550);

        // --- TAB PERMISOS ---
        this.tabPermissions.Controls.Add(this.pnlPermForm);
        this.tabPermissions.Controls.Add(this.pnlPermList);
        this.tabPermissions.Text = "Gestión de Permisos";

        this.pnlPermList.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlPermList.BackColor = System.Drawing.Color.FromArgb(226, 239, 218);
        this.pnlPermList.Controls.Add(this.btnTogglePerm);
        this.pnlPermList.Controls.Add(this.btnEditPerm);
        this.pnlPermList.Controls.Add(this.btnNewPerm);
        this.pnlPermList.Controls.Add(this.dgvPermissions);
        this.pnlPermList.Controls.Add(this.lblTitlePerm);

        this.lblTitlePerm.AutoSize = true;
        this.lblTitlePerm.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.lblTitlePerm.ForeColor = System.Drawing.Color.FromArgb(45, 90, 39);
        this.lblTitlePerm.Location = new System.Drawing.Point(20, 20);
        this.lblTitlePerm.Text = "Listado de Permisos";

        this.btnNewPerm.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        this.btnNewPerm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnNewPerm.ForeColor = System.Drawing.Color.White;
        this.btnNewPerm.Location = new System.Drawing.Point(20, 70);
        this.btnNewPerm.Size = new System.Drawing.Size(120, 35);
        this.btnNewPerm.Text = "+ Nuevo";
        this.btnNewPerm.Click += new System.EventHandler(this.btnNewPerm_Click);

        this.btnEditPerm.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
        this.btnEditPerm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnEditPerm.ForeColor = System.Drawing.Color.White;
        this.btnEditPerm.Location = new System.Drawing.Point(150, 70);
        this.btnEditPerm.Size = new System.Drawing.Size(120, 35);
        this.btnEditPerm.Text = "Editar";
        this.btnEditPerm.Click += new System.EventHandler(this.btnEditPerm_Click);

        this.btnTogglePerm.BackColor = System.Drawing.Color.FromArgb(211, 47, 47);
        this.btnTogglePerm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnTogglePerm.ForeColor = System.Drawing.Color.White;
        this.btnTogglePerm.Location = new System.Drawing.Point(280, 70);
        this.btnTogglePerm.Size = new System.Drawing.Size(140, 35);
        this.btnTogglePerm.Text = "Activar/Desactivar";
        this.btnTogglePerm.Click += new System.EventHandler(this.btnTogglePerm_Click);

        this.dgvPermissions.BackgroundColor = System.Drawing.Color.White;
        this.dgvPermissions.Location = new System.Drawing.Point(20, 120);
        this.dgvPermissions.Size = new System.Drawing.Size(940, 350);
        this.dgvPermissions.ReadOnly = true;
        this.dgvPermissions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvPermissions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

        this.pnlPermForm.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlPermForm.BackColor = System.Drawing.Color.White;
        this.pnlPermForm.Visible = false;
        this.pnlPermForm.Controls.Add(this.btnCancelPerm);
        this.pnlPermForm.Controls.Add(this.btnSavePerm);
        this.pnlPermForm.Controls.Add(this.txtPermDesc);
        this.pnlPermForm.Controls.Add(this.txtPermName);

        this.txtPermName.Location = new System.Drawing.Point(40, 80);
        this.txtPermName.Size = new System.Drawing.Size(300, 30);
        this.txtPermName.PlaceholderText = "Nombre del Permiso";

        this.txtPermDesc.Location = new System.Drawing.Point(40, 130);
        this.txtPermDesc.Size = new System.Drawing.Size(400, 30);
        this.txtPermDesc.PlaceholderText = "Descripción";

        this.btnSavePerm.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
        this.btnSavePerm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSavePerm.ForeColor = System.Drawing.Color.White;
        this.btnSavePerm.Location = new System.Drawing.Point(40, 200);
        this.btnSavePerm.Size = new System.Drawing.Size(120, 40);
        this.btnSavePerm.Text = "Guardar";
        this.btnSavePerm.Click += new System.EventHandler(this.btnSavePerm_Click);

        this.btnCancelPerm.BackColor = System.Drawing.Color.Gray;
        this.btnCancelPerm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnCancelPerm.ForeColor = System.Drawing.Color.White;
        this.btnCancelPerm.Location = new System.Drawing.Point(170, 200);
        this.btnCancelPerm.Size = new System.Drawing.Size(120, 40);
        this.btnCancelPerm.Text = "Cancelar";
        this.btnCancelPerm.Click += new System.EventHandler(this.btnCancelPerm_Click);

        // --- TAB ROLES ---
        this.tabRoles.Controls.Add(this.pnlRoleForm);
        this.tabRoles.Controls.Add(this.pnlRoleList);
        this.tabRoles.Text = "Gestión de Roles";

        this.pnlRoleList.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlRoleList.BackColor = System.Drawing.Color.FromArgb(226, 239, 218);
        this.pnlRoleList.Controls.Add(this.btnToggleRole);
        this.pnlRoleList.Controls.Add(this.btnEditRole);
        this.pnlRoleList.Controls.Add(this.btnNewRole);
        this.pnlRoleList.Controls.Add(this.dgvRoles);
        this.pnlRoleList.Controls.Add(this.lblTitleRole);

        this.lblTitleRole.AutoSize = true;
        this.lblTitleRole.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.lblTitleRole.ForeColor = System.Drawing.Color.FromArgb(45, 90, 39);
        this.lblTitleRole.Location = new System.Drawing.Point(20, 20);
        this.lblTitleRole.Text = "Listado de Roles";

        this.btnNewRole.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        this.btnNewRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnNewRole.ForeColor = System.Drawing.Color.White;
        this.btnNewRole.Location = new System.Drawing.Point(20, 70);
        this.btnNewRole.Size = new System.Drawing.Size(120, 35);
        this.btnNewRole.Text = "+ Nuevo";
        this.btnNewRole.Click += new System.EventHandler(this.btnNewRole_Click);

        this.btnEditRole.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
        this.btnEditRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnEditRole.ForeColor = System.Drawing.Color.White;
        this.btnEditRole.Location = new System.Drawing.Point(150, 70);
        this.btnEditRole.Size = new System.Drawing.Size(120, 35);
        this.btnEditRole.Text = "Editar";
        this.btnEditRole.Click += new System.EventHandler(this.btnEditRole_Click);

        this.btnToggleRole.BackColor = System.Drawing.Color.FromArgb(211, 47, 47);
        this.btnToggleRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnToggleRole.ForeColor = System.Drawing.Color.White;
        this.btnToggleRole.Location = new System.Drawing.Point(280, 70);
        this.btnToggleRole.Size = new System.Drawing.Size(140, 35);
        this.btnToggleRole.Text = "Activar/Desactivar";
        this.btnToggleRole.Click += new System.EventHandler(this.btnToggleRole_Click);

        this.dgvRoles.BackgroundColor = System.Drawing.Color.White;
        this.dgvRoles.Location = new System.Drawing.Point(20, 120);
        this.dgvRoles.Size = new System.Drawing.Size(940, 350);
        this.dgvRoles.ReadOnly = true;
        this.dgvRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvRoles.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

        this.pnlRoleForm.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlRoleForm.BackColor = System.Drawing.Color.White;
        this.pnlRoleForm.Visible = false;
        this.pnlRoleForm.Controls.Add(this.btnCancelRole);
        this.pnlRoleForm.Controls.Add(this.btnSaveRole);
        this.pnlRoleForm.Controls.Add(this.dgvRolePermissions);
        this.pnlRoleForm.Controls.Add(this.txtSearchRolePerms);
        this.pnlRoleForm.Controls.Add(this.lblSearchRolePerms);
        this.pnlRoleForm.Controls.Add(this.txtRoleName);

        this.txtRoleName.Location = new System.Drawing.Point(40, 40);
        this.txtRoleName.Size = new System.Drawing.Size(300, 30);
        this.txtRoleName.PlaceholderText = "Nombre del Rol";

        this.lblSearchRolePerms.Location = new System.Drawing.Point(40, 80);
        this.lblSearchRolePerms.Text = "Buscar Permisos:";
        this.lblSearchRolePerms.AutoSize = true;

        this.txtSearchRolePerms.Location = new System.Drawing.Point(40, 100);
        this.txtSearchRolePerms.Size = new System.Drawing.Size(400, 30);
        this.txtSearchRolePerms.PlaceholderText = "Filtrar...";
        this.txtSearchRolePerms.TextChanged += new System.EventHandler(this.txtSearchRolePerms_TextChanged);

        this.dgvRolePermissions.Location = new System.Drawing.Point(40, 140);
        this.dgvRolePermissions.Size = new System.Drawing.Size(850, 250);
        this.dgvRolePermissions.BackgroundColor = System.Drawing.Color.White;
        this.dgvRolePermissions.AllowUserToAddRows = false;
        this.dgvRolePermissions.RowHeadersVisible = false;
        this.dgvRolePermissions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

        this.btnSaveRole.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
        this.btnSaveRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSaveRole.ForeColor = System.Drawing.Color.White;
        this.btnSaveRole.Location = new System.Drawing.Point(40, 410);
        this.btnSaveRole.Size = new System.Drawing.Size(150, 40);
        this.btnSaveRole.Text = "Guardar Rol";
        this.btnSaveRole.Click += new System.EventHandler(this.btnSaveRole_Click);

        this.btnCancelRole.BackColor = System.Drawing.Color.Gray;
        this.btnCancelRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnCancelRole.ForeColor = System.Drawing.Color.White;
        this.btnCancelRole.Location = new System.Drawing.Point(200, 410);
        this.btnCancelRole.Size = new System.Drawing.Size(120, 40);
        this.btnCancelRole.Text = "Cancelar";
        this.btnCancelRole.Click += new System.EventHandler(this.btnCancelRole_Click);

        // Final Assembly
        this.Controls.Add(this.tabControl);
        this.Name = "SecurityManagementControl";
        this.Size = new System.Drawing.Size(1000, 550);
        this.tabControl.ResumeLayout(false);
        this.tabPermissions.ResumeLayout(false);
        this.tabRoles.ResumeLayout(false);
        this.pnlPermList.ResumeLayout(false);
        this.pnlPermList.PerformLayout();
        this.pnlPermForm.ResumeLayout(false);
        this.pnlPermForm.PerformLayout();
        this.pnlRoleList.ResumeLayout(false);
        this.pnlRoleList.PerformLayout();
        this.pnlRoleForm.ResumeLayout(false);
        this.pnlRoleForm.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvPermissions)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvRolePermissions)).EndInit();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tabPermissions;
    private System.Windows.Forms.TabPage tabRoles;
    
    private System.Windows.Forms.Panel pnlPermList;
    private System.Windows.Forms.DataGridView dgvPermissions;
    private System.Windows.Forms.Button btnNewPerm;
    private System.Windows.Forms.Button btnEditPerm;
    private System.Windows.Forms.Button btnTogglePerm;
    private System.Windows.Forms.Label lblTitlePerm;
    
    private System.Windows.Forms.Panel pnlPermForm;
    private System.Windows.Forms.TextBox txtPermName;
    private System.Windows.Forms.TextBox txtPermDesc;
    private System.Windows.Forms.Button btnSavePerm;
    private System.Windows.Forms.Button btnCancelPerm;

    private System.Windows.Forms.Panel pnlRoleList;
    private System.Windows.Forms.DataGridView dgvRoles;
    private System.Windows.Forms.Button btnNewRole;
    private System.Windows.Forms.Button btnEditRole;
    private System.Windows.Forms.Button btnToggleRole;
    private System.Windows.Forms.Label lblTitleRole;

    private System.Windows.Forms.Panel pnlRoleForm;
    private System.Windows.Forms.TextBox txtRoleName;
    private System.Windows.Forms.TextBox txtSearchRolePerms;
    private System.Windows.Forms.Label lblSearchRolePerms;
    private System.Windows.Forms.DataGridView dgvRolePermissions;
    private System.Windows.Forms.Button btnSaveRole;
    private System.Windows.Forms.Button btnCancelRole;
}
