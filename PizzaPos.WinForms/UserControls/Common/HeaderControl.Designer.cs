namespace PizzaPos.WinForms.UserControls.Common;

partial class HeaderControl
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
        this.lblSystemName = new System.Windows.Forms.Label();
        this.lblUserInfo = new System.Windows.Forms.Label();
        this.btnLogout = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // lblSystemName
        // 
        this.lblSystemName.AutoSize = true;
        this.lblSystemName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblSystemName.ForeColor = System.Drawing.Color.White;
        this.lblSystemName.Location = new System.Drawing.Point(15, 15);
        this.lblSystemName.Name = "lblSystemName";
        this.lblSystemName.Size = new System.Drawing.Size(91, 21);
        this.lblSystemName.TabIndex = 0;
        this.lblSystemName.Text = "PIZZA POS";
        // 
        // lblUserInfo
        // 
        this.lblUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.lblUserInfo.ForeColor = System.Drawing.Color.LightGray;
        this.lblUserInfo.Location = new System.Drawing.Point(600, 15);
        this.lblUserInfo.Name = "lblUserInfo";
        this.lblUserInfo.Size = new System.Drawing.Size(300, 23);
        this.lblUserInfo.TabIndex = 1;
        this.lblUserInfo.Text = "Usuario | Rol";
        this.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // btnLogout
        // 
        this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
        this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnLogout.ForeColor = System.Drawing.Color.White;
        this.btnLogout.Location = new System.Drawing.Point(910, 10);
        this.btnLogout.Name = "btnLogout";
        this.btnLogout.Size = new System.Drawing.Size(75, 30);
        this.btnLogout.TabIndex = 2;
        this.btnLogout.Text = "Salir";
        this.btnLogout.UseVisualStyleBackColor = false;
        this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
        // 
        // HeaderControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
        this.Controls.Add(this.btnLogout);
        this.Controls.Add(this.lblUserInfo);
        this.Controls.Add(this.lblSystemName);
        this.Name = "HeaderControl";
        this.Size = new System.Drawing.Size(1000, 50);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label lblSystemName;
    private System.Windows.Forms.Label lblUserInfo;
    private System.Windows.Forms.Button btnLogout;
}
