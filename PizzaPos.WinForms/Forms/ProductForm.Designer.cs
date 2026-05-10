namespace PizzaPos.WinForms.Forms;

partial class ProductForm
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
        this.lblName = new System.Windows.Forms.Label();
        this.txtName = new System.Windows.Forms.TextBox();
        this.lblPrice = new System.Windows.Forms.Label();
        this.numPrice = new System.Windows.Forms.NumericUpDown();
        this.lblDescription = new System.Windows.Forms.Label();
        this.txtDescription = new System.Windows.Forms.TextBox();
        this.btnSave = new System.Windows.Forms.Button();
        this.btnCancel = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
        this.SuspendLayout();

        // lblName
        this.lblName.Location = new System.Drawing.Point(20, 20);
        this.lblName.Size = new System.Drawing.Size(100, 23);
        this.lblName.Text = "Nombre:";

        // txtName
        this.txtName.Location = new System.Drawing.Point(20, 45);
        this.txtName.Size = new System.Drawing.Size(340, 23);

        // lblPrice
        this.lblPrice.Location = new System.Drawing.Point(20, 80);
        this.lblPrice.Size = new System.Drawing.Size(100, 23);
        this.lblPrice.Text = "Precio:";

        // numPrice
        this.numPrice.DecimalPlaces = 2;
        this.numPrice.Location = new System.Drawing.Point(20, 105);
        this.numPrice.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        this.numPrice.Size = new System.Drawing.Size(120, 23);

        // lblDescription
        this.lblDescription.Location = new System.Drawing.Point(20, 140);
        this.lblDescription.Size = new System.Drawing.Size(100, 23);
        this.lblDescription.Text = "Descripción:";

        // txtDescription
        this.txtDescription.Location = new System.Drawing.Point(20, 165);
        this.txtDescription.Multiline = true;
        this.txtDescription.Size = new System.Drawing.Size(340, 60);

        // btnSave
        this.btnSave.BackColor = System.Drawing.Color.FromArgb(26, 51, 23);
        this.btnSave.ForeColor = System.Drawing.Color.White;
        this.btnSave.Location = new System.Drawing.Point(160, 240);
        this.btnSave.Size = new System.Drawing.Size(100, 35);
        this.btnSave.Text = "Guardar";
        this.btnSave.UseVisualStyleBackColor = false;
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

        // btnCancel
        this.btnCancel.Location = new System.Drawing.Point(265, 240);
        this.btnCancel.Size = new System.Drawing.Size(100, 35);
        this.btnCancel.Text = "Cancelar";
        this.btnCancel.Click += (s, e) => this.Close();

        // ProductForm
        this.ClientSize = new System.Drawing.Size(384, 300);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.txtDescription);
        this.Controls.Add(this.lblDescription);
        this.Controls.Add(this.numPrice);
        this.Controls.Add(this.lblPrice);
        this.Controls.Add(this.txtName);
        this.Controls.Add(this.lblName);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label lblPrice;
    private System.Windows.Forms.NumericUpDown numPrice;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
}
