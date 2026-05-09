namespace PizzaPos.WinForms.Forms;

partial class LoginForm
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
        this.txtUsername = new System.Windows.Forms.TextBox();
        this.txtPassword = new System.Windows.Forms.TextBox();
        this.btnLogin = new System.Windows.Forms.Button();
        this.lblUsername = new System.Windows.Forms.Label();
        this.lblPassword = new System.Windows.Forms.Label();
        this.lblStatus = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // txtUsername
        // 
        this.txtUsername.Location = new System.Drawing.Point(100, 150);
        this.txtUsername.Name = "txtUsername";
        this.txtUsername.Size = new System.Drawing.Size(250, 23);
        this.txtUsername.TabIndex = 0;
        // 
        // txtPassword
        // 
        this.txtPassword.Location = new System.Drawing.Point(100, 220);
        this.txtPassword.Name = "txtPassword";
        this.txtPassword.PasswordChar = '*';
        this.txtPassword.Size = new System.Drawing.Size(250, 23);
        this.txtPassword.TabIndex = 1;
        // 
        // btnLogin
        // 
        this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
        this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnLogin.ForeColor = System.Drawing.Color.White;
        this.btnLogin.Location = new System.Drawing.Point(100, 280);
        this.btnLogin.Name = "btnLogin";
        this.btnLogin.Size = new System.Drawing.Size(250, 40);
        this.btnLogin.TabIndex = 2;
        this.btnLogin.Text = "Iniciar Sesión";
        this.btnLogin.UseVisualStyleBackColor = false;
        this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
        // 
        // lblUsername
        // 
        this.lblUsername.AutoSize = true;
        this.lblUsername.Location = new System.Drawing.Point(100, 130);
        this.lblUsername.Name = "lblUsername";
        this.lblUsername.Size = new System.Drawing.Size(50, 15);
        this.lblUsername.TabIndex = 3;
        this.lblUsername.Text = "Usuario:";
        // 
        // lblPassword
        // 
        this.lblPassword.AutoSize = true;
        this.lblPassword.Location = new System.Drawing.Point(100, 200);
        this.lblPassword.Name = "lblPassword";
        this.lblPassword.Size = new System.Drawing.Size(70, 15);
        this.lblPassword.TabIndex = 4;
        this.lblPassword.Text = "Contraseña:";
        // 
        // lblStatus
        // 
        this.lblStatus.Location = new System.Drawing.Point(100, 330);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(250, 20);
        this.lblStatus.TabIndex = 5;
        this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // LoginForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.White;
        this.ClientSize = new System.Drawing.Size(450, 450);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.lblPassword);
        this.Controls.Add(this.lblUsername);
        this.Controls.Add(this.btnLogin);
        this.Controls.Add(this.txtPassword);
        this.Controls.Add(this.txtUsername);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "LoginForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Pizza POS - Login";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Button btnLogin;
    private System.Windows.Forms.Label lblUsername;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Label lblStatus;
}
