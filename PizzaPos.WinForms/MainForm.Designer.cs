namespace PizzaPos.WinForms;

partial class MainForm
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
        this.lblWelcome = new System.Windows.Forms.Label();
        this.lblRole = new System.Windows.Forms.Label();
        this.btnManageUsers = new System.Windows.Forms.Button();
        this.btnSecurity = new System.Windows.Forms.Button();
        this.btnLogout = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // lblWelcome
        // 
        this.lblWelcome.AutoSize = true;
        this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblWelcome.Location = new System.Drawing.Point(30, 30);
        this.lblWelcome.Name = "lblWelcome";
        this.lblWelcome.Size = new System.Drawing.Size(155, 32);
        this.lblWelcome.TabIndex = 0;
        this.lblWelcome.Text = "¡Bienvenido!";
        // 
        // lblRole
        // 
        this.lblRole.AutoSize = true;
        this.lblRole.Location = new System.Drawing.Point(35, 75);
        this.lblRole.Name = "lblRole";
        this.lblRole.Size = new System.Drawing.Size(31, 15);
        this.lblRole.TabIndex = 1;
        this.lblRole.Text = "Rol: ";
        // 
        // btnManageUsers
        // 
        this.btnManageUsers.Location = new System.Drawing.Point(35, 110);
        this.btnManageUsers.Name = "btnManageUsers";
        this.btnManageUsers.Size = new System.Drawing.Size(200, 35);
        this.btnManageUsers.TabIndex = 2;
        this.btnManageUsers.Text = "Gestionar Usuarios";
        this.btnManageUsers.UseVisualStyleBackColor = true;
        this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
        // 
        // btnSecurity
        // 
        this.btnSecurity.Location = new System.Drawing.Point(35, 155);
        this.btnSecurity.Name = "btnSecurity";
        this.btnSecurity.Size = new System.Drawing.Size(200, 35);
        this.btnSecurity.TabIndex = 4;
        this.btnSecurity.Text = "Roles y Permisos";
        this.btnSecurity.UseVisualStyleBackColor = true;
        this.btnSecurity.Click += new System.EventHandler(this.btnSecurity_Click);
        // 
        // btnLogout
        // 
        this.btnLogout.Location = new System.Drawing.Point(35, 180);
        this.btnLogout.Name = "btnLogout";
        this.btnLogout.Size = new System.Drawing.Size(200, 40);
        this.btnLogout.TabIndex = 3;
        this.btnLogout.Text = "Cerrar Sesión";
        this.btnLogout.UseVisualStyleBackColor = true;
        this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(500, 300);
        this.Controls.Add(this.btnLogout);
        this.Controls.Add(this.btnSecurity);
        this.Controls.Add(this.btnManageUsers);
        this.Controls.Add(this.lblRole);
        this.Controls.Add(this.lblWelcome);
        this.Name = "MainForm";
        this.Text = "Pizza POS - Menú Principal";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label lblWelcome;
    private System.Windows.Forms.Label lblRole;
    private System.Windows.Forms.Button btnManageUsers;
    private System.Windows.Forms.Button btnSecurity;
    private System.Windows.Forms.Button btnLogout;
}
