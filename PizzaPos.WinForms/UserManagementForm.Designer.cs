namespace PizzaPos.WinForms;

partial class UserManagementForm
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
        this.lblTitle = new System.Windows.Forms.Label();
        this.lblUsername = new System.Windows.Forms.Label();
        this.lblPassword = new System.Windows.Forms.Label();
        this.lblConfirmPassword = new System.Windows.Forms.Label();
        this.lblRoles = new System.Windows.Forms.Label();
        this.lblPermissions = new System.Windows.Forms.Label();
        this.txtNewUsername = new System.Windows.Forms.TextBox();
        this.txtNewPassword = new System.Windows.Forms.TextBox();
        this.txtConfirmPassword = new System.Windows.Forms.TextBox();
        this.clbRoles = new System.Windows.Forms.CheckedListBox();
        this.clbPermissions = new System.Windows.Forms.CheckedListBox();
        this.btnSave = new System.Windows.Forms.Button();
        this.dgvUsers = new System.Windows.Forms.DataGridView();
        this.lblUserList = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
        this.SuspendLayout();
        // 
        // lblTitle
        // 
        this.lblTitle.AutoSize = true;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblTitle.Location = new System.Drawing.Point(20, 20);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(184, 25);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "Crear Nuevo Usuario";
        // 
        // lblUsername
        // 
        this.lblUsername.AutoSize = true;
        this.lblUsername.Location = new System.Drawing.Point(25, 70);
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Size = new System.Drawing.Size(50, 15);
        this.lblUsername.TabIndex = 1;
        this.lblUsername.Text = "Usuario:";
        // 
        // lblPassword
        // 
        this.lblPassword.AutoSize = true;
        this.lblPassword.Location = new System.Drawing.Point(25, 110);
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Size = new System.Drawing.Size(70, 15);
        this.lblPassword.TabIndex = 2;
        this.lblPassword.Text = "Contraseña:";
        // 
        // lblConfirmPassword
        // 
        this.lblConfirmPassword.AutoSize = true;
        this.lblConfirmPassword.Location = new System.Drawing.Point(25, 150);
        this.lblConfirmPassword.Name = "lblConfirmPassword";
        this.lblConfirmPassword.Size = new System.Drawing.Size(63, 15);
        this.lblConfirmPassword.TabIndex = 3;
        this.lblConfirmPassword.Text = "Confirmar:";
        // 
        // lblRoles
        // 
        this.lblRoles.AutoSize = true;
        this.lblRoles.Location = new System.Drawing.Point(300, 70);
        this.lblRoles.Name = "lblRoles";
        this.lblRoles.Size = new System.Drawing.Size(38, 15);
        this.lblRoles.TabIndex = 4;
        this.lblRoles.Text = "Roles:";
        // 
        // lblPermissions
        // 
        this.lblPermissions.AutoSize = true;
        this.lblPermissions.Location = new System.Drawing.Point(300, 180);
        this.lblPermissions.Name = "lblPermissions";
        this.lblPermissions.Size = new System.Drawing.Size(117, 15);
        this.lblPermissions.TabIndex = 5;
        this.lblPermissions.Text = "Permisos Adicionales:";
        // 
        // txtNewUsername
        // 
        this.txtNewUsername.Location = new System.Drawing.Point(110, 67);
        this.txtNewUsername.Name = "txtNewUsername";
        this.txtNewUsername.Size = new System.Drawing.Size(150, 23);
        this.txtNewUsername.TabIndex = 6;
        // 
        // txtNewPassword
        // 
        this.txtNewPassword.Location = new System.Drawing.Point(110, 107);
        this.txtNewPassword.Name = "txtNewPassword";
        this.txtNewPassword.PasswordChar = '*';
        this.txtNewPassword.Size = new System.Drawing.Size(150, 23);
        this.txtNewPassword.TabIndex = 7;
        // 
        // txtConfirmPassword
        // 
        this.txtConfirmPassword.Location = new System.Drawing.Point(110, 147);
        this.txtConfirmPassword.Name = "txtConfirmPassword";
        this.txtConfirmPassword.PasswordChar = '*';
        this.txtConfirmPassword.Size = new System.Drawing.Size(150, 23);
        this.txtConfirmPassword.TabIndex = 8;
        // 
        // clbRoles
        // 
        this.clbRoles.FormattingEnabled = true;
        this.clbRoles.Location = new System.Drawing.Point(300, 90);
        this.clbRoles.Name = "clbRoles";
        this.clbRoles.Size = new System.Drawing.Size(200, 76);
        this.clbRoles.TabIndex = 9;
        // 
        // clbPermissions
        // 
        this.clbPermissions.FormattingEnabled = true;
        this.clbPermissions.Location = new System.Drawing.Point(300, 200);
        this.clbPermissions.Name = "clbPermissions";
        this.clbPermissions.Size = new System.Drawing.Size(200, 76);
        this.clbPermissions.TabIndex = 10;
        // 
        // btnSave
        // 
        this.btnSave.Location = new System.Drawing.Point(110, 200);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(150, 35);
        this.btnSave.TabIndex = 11;
        this.btnSave.Text = "Guardar Usuario";
        this.btnSave.UseVisualStyleBackColor = true;
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        // 
        // dgvUsers
        // 
        this.dgvUsers.AllowUserToAddRows = false;
        this.dgvUsers.AllowUserToDeleteRows = false;
        this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvUsers.Location = new System.Drawing.Point(550, 70);
        this.dgvUsers.Name = "dgvUsers";
        this.dgvUsers.ReadOnly = true;
        this.dgvUsers.RowTemplate.Height = 25;
        this.dgvUsers.Size = new System.Drawing.Size(350, 240);
        this.dgvUsers.TabIndex = 12;
        // 
        // lblUserList
        // 
        this.lblUserList.AutoSize = true;
        this.lblUserList.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblUserList.Location = new System.Drawing.Point(550, 25);
        this.lblUserList.Name = "lblUserList";
        this.lblUserList.Size = new System.Drawing.Size(155, 21);
        this.lblUserList.TabIndex = 13;
        this.lblUserList.Text = "Usuarios Existentes";
        // 
        // UserManagementForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(950, 350);
        this.Controls.Add(this.lblUserList);
        this.Controls.Add(this.dgvUsers);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.clbPermissions);
        this.Controls.Add(this.clbRoles);
        this.Controls.Add(this.txtConfirmPassword);
        this.Controls.Add(this.txtNewPassword);
        this.Controls.Add(this.txtNewUsername);
        this.Controls.Add(this.lblPermissions);
        this.Controls.Add(this.lblRoles);
        this.Controls.Add(this.lblConfirmPassword);
        this.Controls.Add(this.lblPassword);
        this.Controls.Add(this.lblUsername);
        this.Controls.Add(this.lblTitle);
        this.Name = "UserManagementForm";
        this.Text = "Pizza POS - Gestión de Usuarios";
        this.Load += new System.EventHandler(this.UserManagementForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Label lblUsername;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Label lblConfirmPassword;
    private System.Windows.Forms.Label lblRoles;
    private System.Windows.Forms.Label lblPermissions;
    private System.Windows.Forms.TextBox txtNewUsername;
    private System.Windows.Forms.TextBox txtNewPassword;
    private System.Windows.Forms.TextBox txtConfirmPassword;
    private System.Windows.Forms.CheckedListBox clbRoles;
    private System.Windows.Forms.CheckedListBox clbPermissions;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.DataGridView dgvUsers;
    private System.Windows.Forms.Label lblUserList;
}
