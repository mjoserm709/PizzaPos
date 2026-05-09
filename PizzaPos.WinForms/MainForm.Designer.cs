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
        this.pnlSidebar = new System.Windows.Forms.Panel();
        this.pnlContent = new System.Windows.Forms.Panel();
        this.lblWelcome = new System.Windows.Forms.Label();
        this.lblRole = new System.Windows.Forms.Label();
        this.btnManageUsers = new System.Windows.Forms.Button();
        this.btnSecurity = new System.Windows.Forms.Button();
        this.btnLogout = new System.Windows.Forms.Button();
        this.pnlSidebar.SuspendLayout();
        this.SuspendLayout();
        // 
        // pnlSidebar
        // 
        this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
        this.pnlSidebar.Controls.Add(this.lblWelcome);
        this.pnlSidebar.Controls.Add(this.lblRole);
        this.pnlSidebar.Controls.Add(this.btnManageUsers);
        this.pnlSidebar.Controls.Add(this.btnSecurity);
        this.pnlSidebar.Controls.Add(this.btnLogout);
        this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
        this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
        this.pnlSidebar.Name = "pnlSidebar";
        this.pnlSidebar.Size = new System.Drawing.Size(250, 600);
        this.pnlSidebar.TabIndex = 0;
        // 
        // pnlContent
        // 
        this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlContent.Location = new System.Drawing.Point(250, 0);
        this.pnlContent.Name = "pnlContent";
        this.pnlContent.Size = new System.Drawing.Size(1000, 600);
        this.pnlContent.TabIndex = 1;
        // 
        // lblWelcome
        // 
        this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblWelcome.ForeColor = System.Drawing.Color.White;
        this.lblWelcome.Location = new System.Drawing.Point(10, 20);
        this.lblWelcome.Name = "lblWelcome";
        this.lblWelcome.Size = new System.Drawing.Size(230, 25);
        this.lblWelcome.Text = "¡Bienvenido!";
        // 
        // lblRole
        // 
        this.lblRole.ForeColor = System.Drawing.Color.LightGray;
        this.lblRole.Location = new System.Drawing.Point(15, 50);
        this.lblRole.Name = "lblRole";
        this.lblRole.Size = new System.Drawing.Size(220, 20);
        this.lblRole.Text = "Rol: Admin";
        // 
        // btnManageUsers
        // 
        this.btnManageUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnManageUsers.ForeColor = System.Drawing.Color.White;
        this.btnManageUsers.Location = new System.Drawing.Point(10, 100);
        this.btnManageUsers.Name = "btnManageUsers";
        this.btnManageUsers.Size = new System.Drawing.Size(230, 45);
        this.btnManageUsers.TabIndex = 2;
        this.btnManageUsers.Text = "Gestionar Usuarios";
        this.btnManageUsers.UseVisualStyleBackColor = true;
        this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
        // 
        // btnSecurity
        // 
        this.btnSecurity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSecurity.ForeColor = System.Drawing.Color.White;
        this.btnSecurity.Location = new System.Drawing.Point(10, 155);
        this.btnSecurity.Name = "btnSecurity";
        this.btnSecurity.Size = new System.Drawing.Size(230, 45);
        this.btnSecurity.TabIndex = 4;
        this.btnSecurity.Text = "Roles y Permisos";
        this.btnSecurity.UseVisualStyleBackColor = true;
        this.btnSecurity.Click += new System.EventHandler(this.btnSecurity_Click);
        // 
        // btnLogout
        // 
        this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnLogout.ForeColor = System.Drawing.Color.White;
        this.btnLogout.Location = new System.Drawing.Point(0, 550);
        this.btnLogout.Name = "btnLogout";
        this.btnLogout.Size = new System.Drawing.Size(250, 50);
        this.btnLogout.TabIndex = 3;
        this.btnLogout.Text = "Cerrar Sesión";
        this.btnLogout.UseVisualStyleBackColor = true;
        this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1250, 600);
        this.Controls.Add(this.pnlContent);
        this.Controls.Add(this.pnlSidebar);
        this.Name = "MainForm";
        this.Text = "Pizza POS - Panel Administrativo";
        this.pnlSidebar.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Panel pnlSidebar;
    private System.Windows.Forms.Panel pnlContent;

    private System.Windows.Forms.Label lblWelcome;
    private System.Windows.Forms.Label lblRole;
    private System.Windows.Forms.Button btnManageUsers;
    private System.Windows.Forms.Button btnSecurity;
    private System.Windows.Forms.Button btnLogout;
}
