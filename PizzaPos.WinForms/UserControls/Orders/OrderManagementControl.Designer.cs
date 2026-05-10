namespace PizzaPos.WinForms.UserControls.Orders;

partial class OrderManagementControl
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TableLayoutPanel pnlBoard;
    private System.Windows.Forms.FlowLayoutPanel flpPendiente;
    private System.Windows.Forms.FlowLayoutPanel flpCocina;
    private System.Windows.Forms.FlowLayoutPanel flpListo;
    private System.Windows.Forms.FlowLayoutPanel flpCamino;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Label lblColPendiente;
    private System.Windows.Forms.Label lblColCocina;
    private System.Windows.Forms.Label lblColListo;
    private System.Windows.Forms.Label lblColCamino;

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
        this.pnlBoard = new System.Windows.Forms.TableLayoutPanel();
        this.flpPendiente = new System.Windows.Forms.FlowLayoutPanel();
        this.flpCocina = new System.Windows.Forms.FlowLayoutPanel();
        this.flpListo = new System.Windows.Forms.FlowLayoutPanel();
        this.flpCamino = new System.Windows.Forms.FlowLayoutPanel();
        this.lblTitle = new System.Windows.Forms.Label();
        this.lblColPendiente = new System.Windows.Forms.Label();
        this.lblColCocina = new System.Windows.Forms.Label();
        this.lblColListo = new System.Windows.Forms.Label();
        this.lblColCamino = new System.Windows.Forms.Label();
        this.SuspendLayout();

        // pnlBoard
        this.pnlBoard.ColumnCount = 4;
        this.pnlBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        this.pnlBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        this.pnlBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        this.pnlBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        this.pnlBoard.Controls.Add(this.lblColPendiente, 0, 0);
        this.pnlBoard.Controls.Add(this.lblColCocina, 1, 0);
        this.pnlBoard.Controls.Add(this.lblColListo, 2, 0);
        this.pnlBoard.Controls.Add(this.lblColCamino, 3, 0);
        this.pnlBoard.Controls.Add(this.flpPendiente, 0, 1);
        this.pnlBoard.Controls.Add(this.flpCocina, 1, 1);
        this.pnlBoard.Controls.Add(this.flpListo, 2, 1);
        this.pnlBoard.Controls.Add(this.flpCamino, 3, 1);
        this.pnlBoard.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlBoard.Location = new System.Drawing.Point(0, 40);
        this.pnlBoard.Name = "pnlBoard";
        this.pnlBoard.RowCount = 2;
        this.pnlBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
        this.pnlBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.pnlBoard.Size = new System.Drawing.Size(1000, 560);

        // flpPendiente
        this.flpPendiente.AutoScroll = true;
        this.flpPendiente.BackColor = System.Drawing.Color.FromArgb(255, 235, 238);
        this.flpPendiente.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flpPendiente.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        this.flpPendiente.WrapContents = false;

        // flpCocina
        this.flpCocina.AutoScroll = true;
        this.flpCocina.BackColor = System.Drawing.Color.FromArgb(255, 249, 196);
        this.flpCocina.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flpCocina.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        this.flpCocina.WrapContents = false;

        // flpListo
        this.flpListo.AutoScroll = true;
        this.flpListo.BackColor = System.Drawing.Color.FromArgb(232, 245, 233);
        this.flpListo.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flpListo.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        this.flpListo.WrapContents = false;

        // flpCamino
        this.flpCamino.AutoScroll = true;
        this.flpCamino.BackColor = System.Drawing.Color.FromArgb(243, 229, 245);
        this.flpCamino.Dock = System.Windows.Forms.DockStyle.Fill;
        this.flpCamino.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        this.flpCamino.WrapContents = false;

        // Labels
        this.lblColPendiente.Text = "📥 PENDIENTES";
        this.lblColPendiente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblColPendiente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lblColPendiente.Dock = System.Windows.Forms.DockStyle.Fill;

        this.lblColCocina.Text = "🔥 EN COCINA";
        this.lblColCocina.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblColCocina.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lblColCocina.Dock = System.Windows.Forms.DockStyle.Fill;

        this.lblColListo.Text = "✅ LISTOS";
        this.lblColListo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblColListo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lblColListo.Dock = System.Windows.Forms.DockStyle.Fill;

        this.lblColCamino.Text = "🛵 EN CAMINO";
        this.lblColCamino.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblColCamino.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lblColCamino.Dock = System.Windows.Forms.DockStyle.Fill;

        // lblTitle
        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.lblTitle.Location = new System.Drawing.Point(0, 0);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(1000, 40);
        this.lblTitle.Text = "TABLERO DE CONTROL DE ORDENES";
        this.lblTitle.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
        this.lblTitle.ForeColor = System.Drawing.Color.White;
        this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        // OrderManagementControl
        this.Controls.Add(this.pnlBoard);
        this.Controls.Add(this.lblTitle);
        this.Name = "OrderManagementControl";
        this.Size = new System.Drawing.Size(1000, 600);
        this.ResumeLayout(false);
    }
}
