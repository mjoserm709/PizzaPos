namespace PizzaPos.WinForms.UserControls.Orders;

partial class OrderCreationControl
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
        this.pnlCustomer = new System.Windows.Forms.Panel();
        this.txtSearchCustomer = new System.Windows.Forms.TextBox();
        this.btnSearchCustomer = new System.Windows.Forms.Button();
        this.lblCustomerInfo = new System.Windows.Forms.Label();
        this.lblTitleCustomer = new System.Windows.Forms.Label();

        this.pnlProducts = new System.Windows.Forms.Panel();
        this.dgvProducts = new System.Windows.Forms.DataGridView();
        this.txtSearchProduct = new System.Windows.Forms.TextBox();
        this.lblTitleProducts = new System.Windows.Forms.Label();

        this.pnlCart = new System.Windows.Forms.Panel();
        this.dgvCart = new System.Windows.Forms.DataGridView();
        this.lblSubtotal = new System.Windows.Forms.Label();
        this.lblTax = new System.Windows.Forms.Label();
        this.lblTotal = new System.Windows.Forms.Label();
        this.btnFinalize = new System.Windows.Forms.Button();
        this.lblTitleCart = new System.Windows.Forms.Label();
        this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
        this.lblPayment = new System.Windows.Forms.Label();

        this.pnlCustomer.SuspendLayout();
        this.pnlProducts.SuspendLayout();
        this.pnlCart.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
        this.SuspendLayout();

        // --- Panel Cliente ---
        this.pnlCustomer.BackColor = System.Drawing.Color.White;
        this.pnlCustomer.Controls.Add(this.lblTitleCustomer);
        this.pnlCustomer.Controls.Add(this.lblCustomerInfo);
        this.pnlCustomer.Controls.Add(this.btnSearchCustomer);
        this.pnlCustomer.Controls.Add(this.txtSearchCustomer);
        this.pnlCustomer.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlCustomer.Location = new System.Drawing.Point(0, 0);
        this.pnlCustomer.Size = new System.Drawing.Size(1000, 100);

        this.lblTitleCustomer.Text = "CLIENTE";
        this.lblTitleCustomer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblTitleCustomer.Location = new System.Drawing.Point(20, 10);
        this.lblTitleCustomer.AutoSize = true;

        this.txtSearchCustomer.Location = new System.Drawing.Point(20, 40);
        this.txtSearchCustomer.Size = new System.Drawing.Size(200, 30);
        this.txtSearchCustomer.PlaceholderText = "Teléfono / Email";

        this.btnSearchCustomer.Text = "Buscar";
        this.btnSearchCustomer.Location = new System.Drawing.Point(230, 40);
        this.btnSearchCustomer.Size = new System.Drawing.Size(80, 30);
        this.btnSearchCustomer.Click += new System.EventHandler(this.btnSearchCustomer_Click);

        this.lblCustomerInfo.Text = "No se ha seleccionado cliente";
        this.lblCustomerInfo.Location = new System.Drawing.Point(330, 45);
        this.lblCustomerInfo.AutoSize = true;
        this.lblCustomerInfo.ForeColor = System.Drawing.Color.DimGray;

        // --- Panel Productos ---
        this.pnlProducts.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
        this.pnlProducts.Controls.Add(this.lblTitleProducts);
        this.pnlProducts.Controls.Add(this.txtSearchProduct);
        this.pnlProducts.Controls.Add(this.dgvProducts);
        this.pnlProducts.Location = new System.Drawing.Point(20, 120);
        this.pnlProducts.Size = new System.Drawing.Size(460, 410);

        this.lblTitleProducts.Text = "PRODUCTOS";
        this.lblTitleProducts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblTitleProducts.Location = new System.Drawing.Point(10, 10);
        this.lblTitleProducts.AutoSize = true;

        this.txtSearchProduct.Location = new System.Drawing.Point(10, 40);
        this.txtSearchProduct.Size = new System.Drawing.Size(440, 30);
        this.txtSearchProduct.PlaceholderText = "Filtrar productos...";
        this.txtSearchProduct.TextChanged += new System.EventHandler(this.txtSearchProduct_TextChanged);

        this.dgvProducts.Location = new System.Drawing.Point(10, 80);
        this.dgvProducts.Size = new System.Drawing.Size(440, 320);
        this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
        this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvProducts.AllowUserToAddRows = false;
        this.dgvProducts.ReadOnly = true;
        this.dgvProducts.RowHeadersVisible = false;
        this.dgvProducts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellDoubleClick);

        // --- Panel Carrito ---
        this.pnlCart.BackColor = System.Drawing.Color.FromArgb(232, 245, 233);
        this.pnlCart.Controls.Add(this.lblPayment);
        this.pnlCart.Controls.Add(this.cmbPaymentMethod);
        this.pnlCart.Controls.Add(this.lblTitleCart);
        this.pnlCart.Controls.Add(this.btnFinalize);
        this.pnlCart.Controls.Add(this.lblTotal);
        this.pnlCart.Controls.Add(this.lblTax);
        this.pnlCart.Controls.Add(this.lblSubtotal);
        this.pnlCart.Controls.Add(this.dgvCart);
        this.pnlCart.Location = new System.Drawing.Point(500, 120);
        this.pnlCart.Size = new System.Drawing.Size(480, 410);

        this.lblTitleCart.Text = "CARRITO DE PEDIDO";
        this.lblTitleCart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblTitleCart.Location = new System.Drawing.Point(10, 10);
        this.lblTitleCart.AutoSize = true;

        this.dgvCart.Location = new System.Drawing.Point(10, 40);
        this.dgvCart.Size = new System.Drawing.Size(460, 200);
        this.dgvCart.BackgroundColor = System.Drawing.Color.White;
        this.dgvCart.AllowUserToAddRows = false;
        this.dgvCart.RowHeadersVisible = false;

        this.lblPayment.Text = "Pago:";
        this.lblPayment.Location = new System.Drawing.Point(10, 255);
        this.lblPayment.AutoSize = true;

        this.cmbPaymentMethod.Location = new System.Drawing.Point(60, 252);
        this.cmbPaymentMethod.Size = new System.Drawing.Size(150, 30);
        this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

        this.lblSubtotal.Text = "Subtotal: $0.00";
        this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.lblSubtotal.Location = new System.Drawing.Point(280, 250);
        this.lblSubtotal.Size = new System.Drawing.Size(180, 25);
        this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        this.lblTax.Text = "IVA (15%): $0.00";
        this.lblTax.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.lblTax.Location = new System.Drawing.Point(280, 280);
        this.lblTax.Size = new System.Drawing.Size(180, 25);
        this.lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        this.lblTotal.Text = "TOTAL: $0.00";
        this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(46, 125, 50);
        this.lblTotal.Location = new System.Drawing.Point(250, 310);
        this.lblTotal.Size = new System.Drawing.Size(210, 35);
        this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        this.btnFinalize.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
        this.btnFinalize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnFinalize.ForeColor = System.Drawing.Color.White;
        this.btnFinalize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.btnFinalize.Text = "FINALIZAR PEDIDO";
        this.btnFinalize.Location = new System.Drawing.Point(10, 355);
        this.btnFinalize.Size = new System.Drawing.Size(460, 45);
        this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);

        // Control principal
        this.Controls.Add(this.pnlCart);
        this.Controls.Add(this.pnlProducts);
        this.Controls.Add(this.pnlCustomer);
        this.Name = "OrderCreationControl";
        this.Size = new System.Drawing.Size(1000, 550);

        this.pnlCustomer.ResumeLayout(false);
        this.pnlCustomer.PerformLayout();
        this.pnlProducts.ResumeLayout(false);
        this.pnlProducts.PerformLayout();
        this.pnlCart.ResumeLayout(false);
        this.pnlCart.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Panel pnlCustomer;
    private System.Windows.Forms.TextBox txtSearchCustomer;
    private System.Windows.Forms.Button btnSearchCustomer;
    private System.Windows.Forms.Label lblCustomerInfo;
    private System.Windows.Forms.Label lblTitleCustomer;

    private System.Windows.Forms.Panel pnlProducts;
    private System.Windows.Forms.DataGridView dgvProducts;
    private System.Windows.Forms.TextBox txtSearchProduct;
    private System.Windows.Forms.Label lblTitleProducts;

    private System.Windows.Forms.Panel pnlCart;
    private System.Windows.Forms.DataGridView dgvCart;
    private System.Windows.Forms.Label lblSubtotal;
    private System.Windows.Forms.Label lblTax;
    private System.Windows.Forms.Label lblTotal;
    private System.Windows.Forms.Button btnFinalize;
    private System.Windows.Forms.Label lblTitleCart;
    private System.Windows.Forms.ComboBox cmbPaymentMethod;
    private System.Windows.Forms.Label lblPayment;
}
