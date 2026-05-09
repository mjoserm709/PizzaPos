namespace PizzaPos.WinForms.Forms;

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
        this.pnlContent = new System.Windows.Forms.Panel();
        this.SuspendLayout();
        // 
        // pnlContent
        // 
        this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(218)))));
        this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlContent.Location = new System.Drawing.Point(0, 0);
        this.pnlContent.Name = "pnlContent";
        this.pnlContent.Size = new System.Drawing.Size(1350, 720);
        this.pnlContent.TabIndex = 0;
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(218)))));
        this.ClientSize = new System.Drawing.Size(1350, 720);
        this.Controls.Add(this.pnlContent);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Pizza POS - Panel de Control";
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Panel pnlContent;
}
