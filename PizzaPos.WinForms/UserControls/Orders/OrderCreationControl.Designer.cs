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
        this.cmbAddress = new System.Windows.Forms.ComboBox();
        this.lblAddress = new System.Windows.Forms.Label();
        this.lstCustomerResults = new System.Windows.Forms.ListBox();
        this.searchTimer = new System.Windows.Forms.Timer();

        this.tblMain = new System.Windows.Forms.TableLayoutPanel();
        this.pnlProducts = new System.Windows.Forms.Panel();
        this.tabsProducts = new System.Windows.Forms.TabControl();
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
        this.txtNotes = new System.Windows.Forms.TextBox();
        this.lblNotesTitle = new System.Windows.Forms.Label();
        this.pnlCartBottom = new System.Windows.Forms.Panel();

        this.pnlCustomer.SuspendLayout();
        this.pnlProducts.SuspendLayout();
        this.pnlCart.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
        this.SuspendLayout();

        // --- Panel Cliente (Arriba) ---
        this.pnlCustomer.BackColor = System.Drawing.Color.White;
        this.pnlCustomer.Controls.Add(this.lblAddress);
        this.pnlCustomer.Controls.Add(this.cmbAddress);
        this.pnlCustomer.Controls.Add(this.lblTitleCustomer);
        this.pnlCustomer.Controls.Add(this.lblCustomerInfo);
        this.pnlCustomer.Controls.Add(this.btnSearchCustomer);
        this.pnlCustomer.Controls.Add(this.txtSearchCustomer);
        this.pnlCustomer.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlCustomer.Location = new System.Drawing.Point(0, 0);
        this.pnlCustomer.Size = new System.Drawing.Size(1200, 100);

        this.lblTitleCustomer.Text = "CLIENTE";
        this.lblTitleCustomer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblTitleCustomer.Location = new System.Drawing.Point(20, 10);
        this.lblTitleCustomer.AutoSize = true;

        this.txtSearchCustomer.Location = new System.Drawing.Point(20, 40);
        this.txtSearchCustomer.Size = new System.Drawing.Size(200, 30);
        this.txtSearchCustomer.PlaceholderText = "Nombre / Teléfono";
        this.txtSearchCustomer.TextChanged += new System.EventHandler(this.txtSearchCustomer_TextChanged);

        this.btnSearchCustomer.Text = "Buscar";
        this.btnSearchCustomer.Location = new System.Drawing.Point(230, 40);
        this.btnSearchCustomer.Size = new System.Drawing.Size(80, 30);
        this.btnSearchCustomer.Click += new System.EventHandler(this.btnSearchCustomer_Click);

        this.lblCustomerInfo.Text = "No se ha seleccionado cliente";
        this.lblCustomerInfo.Location = new System.Drawing.Point(330, 45);
        this.lblCustomerInfo.AutoSize = true;
        this.lblCustomerInfo.ForeColor = System.Drawing.Color.DimGray;

        this.lblAddress.Text = "Dirección de Entrega:";
        this.lblAddress.Location = new System.Drawing.Point(600, 20);
        this.lblAddress.AutoSize = true;
        this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

        this.cmbAddress.Location = new System.Drawing.Point(600, 40);
        this.cmbAddress.Size = new System.Drawing.Size(380, 30);
        this.cmbAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

        this.lstCustomerResults.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.lstCustomerResults.FormattingEnabled = true;
        this.lstCustomerResults.Location = new System.Drawing.Point(20, 70);
        this.lstCustomerResults.Size = new System.Drawing.Size(400, 200);
        this.lstCustomerResults.Visible = false;
        this.lstCustomerResults.DoubleClick += new System.EventHandler(this.lstCustomerResults_DoubleClick);
        this.lstCustomerResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstCustomerResults_KeyDown);

        this.searchTimer.Interval = 400;
        this.searchTimer.Tick += new System.EventHandler(this.searchTimer_Tick);

        // --- Layout Principal ---
        this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tblMain.ColumnCount = 2;
        this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
        this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
        this.tblMain.Controls.Add(this.pnlProducts, 0, 0);
        this.tblMain.Controls.Add(this.pnlCart, 1, 0);
        this.tblMain.Location = new System.Drawing.Point(0, 100);

        // --- Panel Productos ---
        this.pnlProducts.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
        this.pnlProducts.Controls.Add(this.tabsProducts);
        this.pnlProducts.Controls.Add(this.txtSearchProduct);
        this.pnlProducts.Controls.Add(this.lblTitleProducts);
        this.pnlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlProducts.Padding = new System.Windows.Forms.Padding(10);

        this.lblTitleProducts.Text = "PRODUCTOS";
        this.lblTitleProducts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblTitleProducts.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTitleProducts.Height = 35;

        this.txtSearchProduct.Dock = System.Windows.Forms.DockStyle.Top;
        this.txtSearchProduct.PlaceholderText = "Filtrar productos...";
        this.txtSearchProduct.TextChanged += new System.EventHandler(this.txtSearchProduct_TextChanged);

        this.tabsProducts.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabsProducts.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
        this.tabsProducts.Padding = new System.Drawing.Point(10, 5);

        // --- Panel Carrito ---
        this.pnlCart.BackColor = System.Drawing.Color.FromArgb(232, 245, 233);
        this.pnlCart.Controls.Add(this.dgvCart);
        this.pnlCart.Controls.Add(this.pnlCartBottom);
        this.pnlCart.Controls.Add(this.lblTitleCart);
        this.pnlCart.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlCart.Padding = new System.Windows.Forms.Padding(10);

        this.lblTitleCart.Text = "CARRITO DE PEDIDO";
        this.lblTitleCart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblTitleCart.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTitleCart.Height = 35;

        this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvCart.BackgroundColor = System.Drawing.Color.White;
        this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.None;

        // pnlCartBottom
        this.pnlCartBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pnlCartBottom.Height = 180;
        this.pnlCartBottom.Controls.Add(this.lblPayment);
        this.pnlCartBottom.Controls.Add(this.cmbPaymentMethod);
        this.pnlCartBottom.Controls.Add(this.lblNotesTitle);
        this.pnlCartBottom.Controls.Add(this.txtNotes);
        this.pnlCartBottom.Controls.Add(this.lblSubtotal);
        this.pnlCartBottom.Controls.Add(this.lblTax);
        this.pnlCartBottom.Controls.Add(this.lblTotal);
        this.pnlCartBottom.Controls.Add(this.btnFinalize);

        this.lblPayment.Text = "Pago:";
        this.lblPayment.Location = new System.Drawing.Point(0, 10);
        this.lblPayment.AutoSize = true;

        this.cmbPaymentMethod.Location = new System.Drawing.Point(50, 7);
        this.cmbPaymentMethod.Size = new System.Drawing.Size(120, 30);

        this.lblNotesTitle.Text = "Notas del Pedido:";
        this.lblNotesTitle.Location = new System.Drawing.Point(0, 45);
        this.lblNotesTitle.AutoSize = true;

        this.txtNotes.Location = new System.Drawing.Point(0, 65);
        this.txtNotes.Size = new System.Drawing.Size(170, 50);
        this.txtNotes.Multiline = true;

        this.lblSubtotal.Text = "Subtotal: $0.00";
        this.lblSubtotal.Location = new System.Drawing.Point(260, 10);
        this.lblSubtotal.Size = new System.Drawing.Size(180, 25);
        this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        this.lblTax.Text = "IVA (15%): $0.00";
        this.lblTax.Location = new System.Drawing.Point(260, 40);
        this.lblTax.Size = new System.Drawing.Size(180, 25);
        this.lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        this.lblTotal.Text = "TOTAL: $0.00";
        this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblTotal.Location = new System.Drawing.Point(260, 70);
        this.lblTotal.Size = new System.Drawing.Size(180, 30);
        this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        this.btnFinalize.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
        this.btnFinalize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnFinalize.ForeColor = System.Drawing.Color.White;
        this.btnFinalize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.btnFinalize.Text = "FINALIZAR PEDIDO";
        this.btnFinalize.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.btnFinalize.Height = 50;
        this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);

        // --- OrderCreationControl ---
        this.Controls.Add(this.lstCustomerResults);
        this.Controls.Add(this.tblMain);
        this.Controls.Add(this.pnlCustomer);
        this.Name = "OrderCreationControl";
        this.Size = new System.Drawing.Size(1200, 700);
        this.Dock = System.Windows.Forms.DockStyle.Fill;

        this.pnlCustomer.ResumeLayout(false);
        this.pnlCustomer.PerformLayout();
        this.pnlProducts.ResumeLayout(false);
        this.pnlProducts.PerformLayout();
        this.pnlCart.ResumeLayout(false);
        this.pnlCart.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Panel pnlCustomer;
    private System.Windows.Forms.TextBox txtSearchCustomer;
    private System.Windows.Forms.Button btnSearchCustomer;
    private System.Windows.Forms.Label lblCustomerInfo;
    private System.Windows.Forms.Label lblTitleCustomer;
    private System.Windows.Forms.ComboBox cmbAddress;
    private System.Windows.Forms.Label lblAddress;
    private System.Windows.Forms.ListBox lstCustomerResults;
    private System.Windows.Forms.Timer searchTimer;

    private System.Windows.Forms.TableLayoutPanel tblMain;
    private System.Windows.Forms.Panel pnlProducts;
    private System.Windows.Forms.TabControl tabsProducts;
    private System.Windows.Forms.TextBox txtSearchProduct;
    private System.Windows.Forms.Label lblTitleProducts;

    private System.Windows.Forms.Panel pnlCart;
    private System.Windows.Forms.Panel pnlCartBottom;
    private System.Windows.Forms.DataGridView dgvCart;
    private System.Windows.Forms.Label lblSubtotal;
    private System.Windows.Forms.Label lblTax;
    private System.Windows.Forms.Label lblTotal;
    private System.Windows.Forms.Button btnFinalize;
    private System.Windows.Forms.Label lblTitleCart;
    private System.Windows.Forms.ComboBox cmbPaymentMethod;
    private System.Windows.Forms.Label lblPayment;
    private System.Windows.Forms.TextBox txtNotes;
    private System.Windows.Forms.Label lblNotesTitle;
}
