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
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tabPermissions = new System.Windows.Forms.TabPage();
        this.tabRoles = new System.Windows.Forms.TabPage();
        this.lblPermName = new System.Windows.Forms.Label();
        this.txtPermName = new System.Windows.Forms.TextBox();
        this.lblPermDesc = new System.Windows.Forms.Label();
        this.txtPermDesc = new System.Windows.Forms.TextBox();
        this.btnCreatePermission = new System.Windows.Forms.Button();
        this.lblRoleName = new System.Windows.Forms.Label();
        this.txtRoleName = new System.Windows.Forms.TextBox();
        this.lblRolePermissions = new System.Windows.Forms.Label();
        this.clbRolePermissions = new System.Windows.Forms.CheckedListBox();
        this.btnCreateRole = new System.Windows.Forms.Button();
        this.tabControl1.SuspendLayout();
        this.tabPermissions.SuspendLayout();
        this.tabRoles.SuspendLayout();
        this.SuspendLayout();
        // 
        // tabControl1
        // 
        this.tabControl1.Controls.Add(this.tabPermissions);
        this.tabControl1.Controls.Add(this.tabRoles);
        this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabControl1.Location = new System.Drawing.Point(0, 0);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new System.Drawing.Size(600, 450);
        this.tabControl1.TabIndex = 0;
        // 
        // tabPermissions
        // 
        this.tabPermissions.Controls.Add(this.btnCreatePermission);
        this.tabPermissions.Controls.Add(this.txtPermDesc);
        this.tabPermissions.Controls.Add(this.lblPermDesc);
        this.tabPermissions.Controls.Add(this.txtPermName);
        this.tabPermissions.Controls.Add(this.lblPermName);
        this.tabPermissions.Location = new System.Drawing.Point(4, 24);
        this.tabPermissions.Name = "tabPermissions";
        this.tabPermissions.Padding = new System.Windows.Forms.Padding(3);
        this.tabPermissions.Size = new System.Drawing.Size(592, 422);
        this.tabPermissions.TabIndex = 0;
        this.tabPermissions.Text = "Permisos";
        this.tabPermissions.UseVisualStyleBackColor = true;
        // 
        // tabRoles
        // 
        this.tabRoles.Controls.Add(this.btnCreateRole);
        this.tabRoles.Controls.Add(this.clbRolePermissions);
        this.tabRoles.Controls.Add(this.lblRolePermissions);
        this.tabRoles.Controls.Add(this.txtRoleName);
        this.tabRoles.Controls.Add(this.lblRoleName);
        this.tabRoles.Location = new System.Drawing.Point(4, 24);
        this.tabRoles.Name = "tabRoles";
        this.tabRoles.Padding = new System.Windows.Forms.Padding(3);
        this.tabRoles.Size = new System.Drawing.Size(592, 422);
        this.tabRoles.TabIndex = 1;
        this.tabRoles.Text = "Roles";
        this.tabRoles.UseVisualStyleBackColor = true;
        // 
        // lblPermName
        // 
        this.lblPermName.AutoSize = true;
        this.lblPermName.Location = new System.Drawing.Point(20, 20);
        this.lblPermName.Name = "lblPermName";
        this.lblPermName.Size = new System.Drawing.Size(54, 15);
        this.lblPermName.TabIndex = 0;
        this.lblPermName.Text = "Nombre:";
        // 
        // txtPermName
        // 
        this.txtPermName.Location = new System.Drawing.Point(100, 17);
        this.txtPermName.Name = "txtPermName";
        this.txtPermName.Size = new System.Drawing.Size(200, 23);
        this.txtPermName.TabIndex = 1;
        // 
        // lblPermDesc
        // 
        this.lblPermDesc.AutoSize = true;
        this.lblPermDesc.Location = new System.Drawing.Point(20, 60);
        this.lblPermDesc.Name = "lblPermDesc";
        this.lblPermDesc.Size = new System.Drawing.Size(72, 15);
        this.lblPermDesc.TabIndex = 2;
        this.lblPermDesc.Text = "Descripción:";
        // 
        // txtPermDesc
        // 
        this.txtPermDesc.Location = new System.Drawing.Point(100, 57);
        this.txtPermDesc.Name = "txtPermDesc";
        this.txtPermDesc.Size = new System.Drawing.Size(200, 23);
        this.txtPermDesc.TabIndex = 3;
        // 
        // btnCreatePermission
        // 
        this.btnCreatePermission.Location = new System.Drawing.Point(100, 100);
        this.btnCreatePermission.Name = "btnCreatePermission";
        this.btnCreatePermission.Size = new System.Drawing.Size(200, 30);
        this.btnCreatePermission.TabIndex = 4;
        this.btnCreatePermission.Text = "Crear Permiso";
        this.btnCreatePermission.UseVisualStyleBackColor = true;
        this.btnCreatePermission.Click += new System.EventHandler(this.btnCreatePermission_Click);
        // 
        // lblRoleName
        // 
        this.lblRoleName.AutoSize = true;
        this.lblRoleName.Location = new System.Drawing.Point(20, 20);
        this.lblRoleName.Name = "lblRoleName";
        this.lblRoleName.Size = new System.Drawing.Size(54, 15);
        this.lblRoleName.TabIndex = 0;
        this.lblRoleName.Text = "Nombre:";
        // 
        // txtRoleName
        // 
        this.txtRoleName.Location = new System.Drawing.Point(100, 17);
        this.txtRoleName.Name = "txtRoleName";
        this.txtRoleName.Size = new System.Drawing.Size(200, 23);
        this.txtRoleName.TabIndex = 1;
        // 
        // lblRolePermissions
        // 
        this.lblRolePermissions.AutoSize = true;
        this.lblRolePermissions.Location = new System.Drawing.Point(20, 60);
        this.lblRolePermissions.Name = "lblRolePermissions";
        this.lblRolePermissions.Size = new System.Drawing.Size(58, 15);
        this.lblRolePermissions.TabIndex = 2;
        this.lblRolePermissions.Text = "Permisos:";
        // 
        // clbRolePermissions
        // 
        this.clbRolePermissions.FormattingEnabled = true;
        this.clbRolePermissions.Location = new System.Drawing.Point(100, 60);
        this.clbRolePermissions.Name = "clbRolePermissions";
        this.clbRolePermissions.Size = new System.Drawing.Size(200, 130);
        this.clbRolePermissions.TabIndex = 3;
        // 
        // btnCreateRole
        // 
        this.btnCreateRole.Location = new System.Drawing.Point(100, 210);
        this.btnCreateRole.Name = "btnCreateRole";
        this.btnCreateRole.Size = new System.Drawing.Size(200, 30);
        this.btnCreateRole.TabIndex = 4;
        this.btnCreateRole.Text = "Crear Rol";
        this.btnCreateRole.UseVisualStyleBackColor = true;
        this.btnCreateRole.Click += new System.EventHandler(this.btnCreateRole_Click);
        // 
        // SecurityManagementControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.tabControl1);
        this.Name = "SecurityManagementControl";
        this.Size = new System.Drawing.Size(600, 450);
        this.tabControl1.ResumeLayout(false);
        this.tabPermissions.ResumeLayout(false);
        this.tabPermissions.PerformLayout();
        this.tabRoles.ResumeLayout(false);
        this.tabRoles.PerformLayout();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPermissions;
    private System.Windows.Forms.TabPage tabRoles;
    private System.Windows.Forms.Label lblPermName;
    private System.Windows.Forms.TextBox txtPermName;
    private System.Windows.Forms.Label lblPermDesc;
    private System.Windows.Forms.TextBox txtPermDesc;
    private System.Windows.Forms.Button btnCreatePermission;
    private System.Windows.Forms.Label lblRoleName;
    private System.Windows.Forms.TextBox txtRoleName;
    private System.Windows.Forms.Label lblRolePermissions;
    private System.Windows.Forms.CheckedListBox clbRolePermissions;
    private System.Windows.Forms.Button btnCreateRole;
}
