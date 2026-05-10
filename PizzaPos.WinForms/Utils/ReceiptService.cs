using System.Drawing;
using System.Drawing.Printing;
using PizzaPos.WinForms.Models;

namespace PizzaPos.WinForms.Utils;

public static class ReceiptService
{
    private static OrderResponseDto? _currentOrder;
    private static readonly Font TitleFont = new Font("Segoe UI", 14, FontStyle.Bold);
    private static readonly Font HeaderFont = new Font("Segoe UI", 9, FontStyle.Bold);
    private static readonly Font BodyFont = new Font("Segoe UI", 9);
    private static readonly Font SmallFont = new Font("Segoe UI", 7);
    private static readonly Font FooterFont = new Font("Segoe UI", 8, FontStyle.Italic);

    public static void Print(OrderResponseDto order)
    {
        _currentOrder = order;
        PrintDocument pd = new PrintDocument();
        pd.PrintPage += PrintPageHandler;
        
        // Configurar tamaño para ticket térmico (80mm aprox 300 centésimas de pulgada)
        pd.DefaultPageSettings.PaperSize = new PaperSize("Thermal", 300, 1000);
        
        try
        {
            // En un entorno real, podrías usar pd.Print() directamente
            // Para propósitos de este demo y para que el usuario lo vea, usaremos el diálogo de vista previa
            var preview = new System.Windows.Forms.PrintPreviewDialog();
            preview.Document = pd;
            preview.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            preview.ShowDialog();
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show("Error al imprimir: " + ex.Message);
        }
    }

    private static void PrintPageHandler(object sender, PrintPageEventArgs e)
    {
        if (_currentOrder == null || e.Graphics == null) return;

        Graphics g = e.Graphics;
        float y = 20;
        float width = 280; // Margen de 10 a cada lado
        float x = 10;
        StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
        StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far };

        // 1. LOGO / TÍTULO
        g.DrawString("PIZZA POS", TitleFont, Brushes.Black, new RectangleF(x, y, width, 30), centerFormat);
        y += 30;
        g.DrawString("¡Sabor Real en Cada Rebanada!", FooterFont, Brushes.Black, new RectangleF(x, y, width, 20), centerFormat);
        y += 25;

        // 2. INFO DEL NEGOCIO
        g.DrawString("Calle Principal #123, Ciudad", SmallFont, Brushes.Black, new RectangleF(x, y, width, 15), centerFormat);
        y += 15;
        g.DrawString("Tel: (504) 2222-3344", SmallFont, Brushes.Black, new RectangleF(x, y, width, 15), centerFormat);
        y += 20;

        g.DrawLine(Pens.Black, x, y, width, y);
        y += 10;

        // 3. INFO DE LA ORDEN
        g.DrawString($"ORDEN: {_currentOrder.OrderNumber}", HeaderFont, Brushes.Black, x, y);
        y += 20;
        g.DrawString($"FECHA: {_currentOrder.CreatedAt:dd/MM/yyyy HH:mm}", BodyFont, Brushes.Black, x, y);
        y += 20;
        g.DrawString($"CLIENTE: {_currentOrder.CustomerName}", BodyFont, Brushes.Black, x, y);
        y += 20;
        if (!string.IsNullOrEmpty(_currentOrder.CustomerPhone))
        {
            g.DrawString($"TEL: {_currentOrder.CustomerPhone}", BodyFont, Brushes.Black, x, y);
            y += 20;
        }
        
        g.DrawLine(Pens.Black, x, y, width, y);
        y += 10;

        // 4. CABECERA DE PRODUCTOS
        g.DrawString("DESCRIPCIÓN", HeaderFont, Brushes.Black, x, y);
        g.DrawString("TOTAL", HeaderFont, Brushes.Black, width, y, rightFormat);
        y += 20;

        // 5. LISTA DE PRODUCTOS
        foreach (var item in _currentOrder.Details)
        {
            string line = $"{item.Quantity}x {item.ProductName}";
            // Si el nombre es muy largo, cortarlo o envolverlo (aquí lo simplificamos)
            g.DrawString(line, BodyFont, Brushes.Black, x, y);
            g.DrawString(item.Total.ToString("C2"), BodyFont, Brushes.Black, width, y, rightFormat);
            y += 20;
        }

        y += 10;
        g.DrawLine(Pens.Black, x, y, width, y);
        y += 10;

        // 6. TOTALES
        g.DrawString("SUBTOTAL:", BodyFont, Brushes.Black, width - 100, y, rightFormat);
        g.DrawString(_currentOrder.Subtotal.ToString("C2"), BodyFont, Brushes.Black, width, y, rightFormat);
        y += 20;

        g.DrawString("IVA:", BodyFont, Brushes.Black, width - 100, y, rightFormat);
        g.DrawString(_currentOrder.TaxAmount.ToString("C2"), BodyFont, Brushes.Black, width, y, rightFormat);
        y += 20;

        g.DrawString("TOTAL:", HeaderFont, Brushes.Black, width - 100, y, rightFormat);
        g.DrawString(_currentOrder.Total.ToString("C2"), HeaderFont, Brushes.Black, width, y, rightFormat);
        y += 30;

        // 7. NOTAS
        if (!string.IsNullOrEmpty(_currentOrder.Notes))
        {
            g.DrawString("NOTAS:", HeaderFont, Brushes.Black, x, y);
            y += 20;
            g.DrawString(_currentOrder.Notes, SmallFont, Brushes.Black, new RectangleF(x, y, width, 40));
            y += 45;
        }

        // 8. PIE DE PÁGINA
        g.DrawString("¡GRACIAS POR SU COMPRA!", HeaderFont, Brushes.Black, new RectangleF(x, y, width, 20), centerFormat);
        y += 20;
        g.DrawString("Conserve su recibo para cualquier reclamo.", SmallFont, Brushes.Gray, new RectangleF(x, y, width, 15), centerFormat);
        
        e.HasMorePages = false;
    }
}
