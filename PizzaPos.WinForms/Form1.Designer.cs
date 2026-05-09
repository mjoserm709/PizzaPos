namespace PizzaPos.WinForms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.btnCheckStatus = new System.Windows.Forms.Button();
        this.lblStatus = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // btnCheckStatus
        // 
        this.btnCheckStatus.Location = new System.Drawing.Point(12, 12);
        this.btnCheckStatus.Name = "btnCheckStatus";
        this.btnCheckStatus.Size = new System.Drawing.Size(150, 40);
        this.btnCheckStatus.TabIndex = 0;
        this.btnCheckStatus.Text = "Check API Status";
        this.btnCheckStatus.UseVisualStyleBackColor = true;
        this.btnCheckStatus.Click += new System.EventHandler(this.btnCheckStatus_Click);
        // 
        // lblStatus
        // 
        this.lblStatus.AutoSize = true;
        this.lblStatus.Location = new System.Drawing.Point(12, 65);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(95, 15);
        this.lblStatus.TabIndex = 1;
        this.lblStatus.Text = "Status: Unknown";
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 150);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.btnCheckStatus);
        this.Name = "Form1";
        this.Text = "Pizza POS - Frontend";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Button btnCheckStatus;
    private System.Windows.Forms.Label lblStatus;

    #endregion
}
