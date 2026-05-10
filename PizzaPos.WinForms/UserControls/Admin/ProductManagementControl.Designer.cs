namespace PizzaPos.WinForms.UserControls.Admin;

partial class ProductManagementControl
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
        this.dgvProducts = new System.Windows.Forms.DataGridView();
        this.pnlActions = new System.Windows.Forms.Panel();
        this.btnToggleProduct = new System.Windows.Forms.Button();
        this.btnEditProduct = new System.Windows.Forms.Button();
        this.btnNewProduct = new System.Windows.Forms.Button();
        this.lblTitle = new System.Windows.Forms.Label();

        ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
        this.pnlActions.SuspendLayout();
        this.SuspendLayout();

        // dgvProducts
        this.dgvProducts.AllowUserToAddRows = false;
        this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
        this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvProducts.Location = new System.Drawing.Point(0, 80);
        this.dgvProducts.Name = "dgvProducts";
        this.dgvProducts.ReadOnly = true;
        this.dgvProducts.RowHeadersVisible = false;
        this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvProducts.Size = new System.Drawing.Size(800, 370);

        // pnlActions
        this.pnlActions.Controls.Add(this.btnToggleProduct);
        this.pnlActions.Controls.Add(this.btnEditProduct);
        this.pnlActions.Controls.Add(this.btnNewProduct);
        this.pnlActions.Controls.Add(this.lblTitle);
        this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlActions.Location = new System.Drawing.Point(0, 0);
        this.pnlActions.Name = "pnlActions";
        this.pnlActions.Size = new System.Drawing.Size(800, 80);

        // btnToggleProduct
        this.btnToggleProduct.BackColor = System.Drawing.Color.FromArgb(26, 51, 23);
        this.btnToggleProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnToggleProduct.ForeColor = System.Drawing.Color.White;
        this.btnToggleProduct.Location = new System.Drawing.Point(680, 40);
        this.btnToggleProduct.Name = "btnToggleProduct";
        this.btnToggleProduct.Size = new System.Drawing.Size(100, 30);
        this.btnToggleProduct.Text = "Activar/Des";
        this.btnToggleProduct.UseVisualStyleBackColor = false;
        this.btnToggleProduct.Click += new System.EventHandler(this.btnToggleProduct_Click);

        // btnEditProduct
        this.btnEditProduct.BackColor = System.Drawing.Color.FromArgb(26, 51, 23);
        this.btnEditProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnEditProduct.ForeColor = System.Drawing.Color.White;
        this.btnEditProduct.Location = new System.Drawing.Point(570, 40);
        this.btnEditProduct.Name = "btnEditProduct";
        this.btnEditProduct.Size = new System.Drawing.Size(100, 30);
        this.btnEditProduct.Text = "Editar";
        this.btnEditProduct.UseVisualStyleBackColor = false;
        this.btnEditProduct.Click += new System.EventHandler(this.btnEditProduct_Click);

        // btnNewProduct
        this.btnNewProduct.BackColor = System.Drawing.Color.FromArgb(26, 51, 23);
        this.btnNewProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnNewProduct.ForeColor = System.Drawing.Color.White;
        this.btnNewProduct.Location = new System.Drawing.Point(460, 40);
        this.btnNewProduct.Name = "btnNewProduct";
        this.btnNewProduct.Size = new System.Drawing.Size(100, 30);
        this.btnNewProduct.Text = "Nuevo";
        this.btnNewProduct.UseVisualStyleBackColor = false;
        this.btnNewProduct.Click += new System.EventHandler(this.btnNewProduct_Click);

        // lblTitle
        this.lblTitle.AutoSize = true;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.lblTitle.Location = new System.Drawing.Point(15, 10);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(280, 30);
        this.lblTitle.Text = "GESTIÓN DE PRODUCTOS";

        // ProductManagementControl
        this.Controls.Add(this.dgvProducts);
        this.Controls.Add(this.pnlActions);
        this.Name = "ProductManagementControl";
        this.Size = new System.Drawing.Size(800, 450);
        ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
        this.pnlActions.ResumeLayout(false);
        this.pnlActions.PerformLayout();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.DataGridView dgvProducts;
    private System.Windows.Forms.Panel pnlActions;
    private System.Windows.Forms.Button btnToggleProduct;
    private System.Windows.Forms.Button btnEditProduct;
    private System.Windows.Forms.Button btnNewProduct;
    private System.Windows.Forms.Label lblTitle;
}
