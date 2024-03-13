using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

class Program
{
    static void Main()
    {
        PdfDocument document = new PdfDocument();
        PdfPage page = document.AddPage();
        page.Width = XUnit.FromMillimeter(83);
        XFont fontTitulo = new XFont("Arial", 12);
        page.Height = XUnit.FromMillimeter(document.Pages[0].Height.Value);
        XGraphics gfx = XGraphics.FromPdfPage(page);
        XStringFormat format = new XStringFormat();
        XPoint startPoint = new XPoint(10, 10);

        DrawLine(gfx, ref startPoint, 10);

        DrawTitle("CIERRE DE CAJA", gfx, fontTitulo, page, ref startPoint, 20, format);

        DrawLine(gfx, ref startPoint, 10);

        string directorioProyecto = AppDomain.CurrentDomain.BaseDirectory;
        string rutaCompleta = Path.Combine(directorioProyecto, "Factura.pdf");

        using (MemoryStream stream = new MemoryStream())
        {
            document.Save(stream, closeStream: false);
            byte[] pdfBytes = stream.ToArray();
        }

        document.Save(rutaCompleta);
        document.Close();
    }

    static void DrawLine(XGraphics gfx, ref XPoint startPoint, double startPointParam)
    {
        gfx.DrawLine(XPens.Black, 10, startPoint.Y + 2, 220, startPoint.Y + 2);
        startPoint.Y += startPointParam;
    }

    static void DrawTitle(string titulo, XGraphics gfx, XFont fontTitulo, PdfPage page, ref XPoint startPoint, double paramY, XStringFormat format)
    {
        XSize titleSize = gfx.MeasureString(titulo, fontTitulo);
        XPoint titleStartPoint = new XPoint((page.Width - titleSize.Width) / 2, startPoint.Y);
        XFont boldFont = new XFont("Arial", 12, XFontStyle.Bold);
        gfx.DrawString(titulo, boldFont, XBrushes.Black, titleStartPoint, format);
        startPoint.Y += paramY;
    }
}