namespace PizzaPos.WinForms.UserControls.Common;

partial class SidebarControl
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
        this.btnNewOrder = new System.Windows.Forms.Button();
        this.btnActiveOrders = new System.Windows.Forms.Button();
        this.btnHistory = new System.Windows.Forms.Button();
        this.btnSettings = new System.Windows.Forms.Button();
        this.btnManageUsers = new System.Windows.Forms.Button();
        this.btnSecurity = new System.Windows.Forms.Button();
        this.lblMenu = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // lblMenu
        // 
        this.lblMenu.AutoSize = true;
        this.lblMenu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblMenu.ForeColor = System.Drawing.Color.Gray;
        this.lblMenu.Location = new System.Drawing.Point(15, 20);
        this.lblMenu.Name = "lblMenu";
        this.lblMenu.Size = new System.Drawing.Size(51, 19);
        this.lblMenu.TabIndex = 0;
        this.lblMenu.Text = "MENÚ";
        // 
        // btnNewOrder
        // 
        this.btnNewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnNewOrder.ForeColor = System.Drawing.Color.White;
        this.btnNewOrder.Location = new System.Drawing.Point(10, 60);
        this.btnNewOrder.Name = "btnNewOrder";
        this.btnNewOrder.Size = new System.Drawing.Size(180, 40);
        this.btnNewOrder.TabIndex = 1;
        this.btnNewOrder.Text = "Nueva Orden";
        this.btnNewOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnNewOrder.Click += new System.EventHandler(this.btnNewOrder_Click);
        // 
        // btnActiveOrders
        // 
        this.btnActiveOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnActiveOrders.ForeColor = System.Drawing.Color.White;
        this.btnActiveOrders.Location = new System.Drawing.Point(10, 105);
        this.btnActiveOrders.Name = "btnActiveOrders";
        this.btnActiveOrders.Size = new System.Drawing.Size(180, 40);
        this.btnActiveOrders.TabIndex = 2;
        this.btnActiveOrders.Text = "Órdenes Activas";
        this.btnActiveOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnActiveOrders.Click += new System.EventHandler(this.btnActiveOrders_Click);
        // 
        // btnHistory
        // 
        this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnHistory.ForeColor = System.Drawing.Color.White;
        this.btnHistory.Location = new System.Drawing.Point(10, 150);
        this.btnHistory.Name = "btnHistory";
        this.btnHistory.Size = new System.Drawing.Size(180, 40);
        this.btnHistory.TabIndex = 3;
        this.btnHistory.Text = "Historial";
        this.btnHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
        // 
        // btnSettings
        // 
        this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSettings.ForeColor = System.Drawing.Color.White;
        this.btnSettings.Location = new System.Drawing.Point(10, 195);
        this.btnSettings.Name = "btnSettings";
        this.btnSettings.Size = new System.Drawing.Size(180, 40);
        this.btnSettings.TabIndex = 4;
        this.btnSettings.Text = "Configuración";
        this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
        // 
        // btnManageUsers
        // 
        this.btnManageUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnManageUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
        this.btnManageUsers.Location = new System.Drawing.Point(10, 260);
        this.btnManageUsers.Name = "btnManageUsers";
        this.btnManageUsers.Size = new System.Drawing.Size(180, 40);
        this.btnManageUsers.TabIndex = 5;
        this.btnManageUsers.Text = "Gestionar Usuarios";
        this.btnManageUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
        // 
        // btnSecurity
        // 
        this.btnSecurity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSecurity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
        this.btnSecurity.Location = new System.Drawing.Point(10, 305);
        this.btnSecurity.Name = "btnSecurity";
        this.btnSecurity.Size = new System.Drawing.Size(180, 40);
        this.btnSecurity.TabIndex = 6;
        this.btnSecurity.Text = "Seguridad";
        this.btnSecurity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnSecurity.Click += new System.EventHandler(this.btnSecurity_Click);
        // 
        // SidebarControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
        this.Controls.Add(this.btnSecurity);
        this.Controls.Add(this.btnManageUsers);
        this.Controls.Add(this.btnSettings);
        this.Controls.Add(this.btnHistory);
        this.Controls.Add(this.btnActiveOrders);
        this.Controls.Add(this.btnNewOrder);
        this.Controls.Add(this.lblMenu);
        this.Name = "SidebarControl";
        this.Size = new System.Drawing.Size(200, 500);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Button btnNewOrder;
    private System.Windows.Forms.Button btnActiveOrders;
    private System.Windows.Forms.Button btnHistory;
    private System.Windows.Forms.Button btnSettings;
    private System.Windows.Forms.Button btnManageUsers;
    private System.Windows.Forms.Button btnSecurity;
    private System.Windows.Forms.Label lblMenu;
}
