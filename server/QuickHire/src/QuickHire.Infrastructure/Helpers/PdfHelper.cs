using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.IO.Image;
using Microsoft.AspNetCore.Http;
using System.IO;
using QuickHire.Application.Orders.Models.Invoice;
using QuickHire.Infrastructure.Helpers;
using iText.Layout.Borders;
using iText.Kernel.Font;
using iText.IO.Font.Constants;

internal class PdfHelper : IPdfHelper
{
    public MemoryStream GeneratePdfStream(InvoiceModel invoice)
    {
        var memoryStream = new MemoryStream();

        using (var writer = new PdfWriter(memoryStream))
        using (var pdf = new PdfDocument(writer))
        using (var document = new Document(pdf))
        {
            var black = ColorConstants.BLACK;
            var gray = ColorConstants.GRAY;

            PdfFont regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            var headerTable = new Table(UnitValue.CreatePercentArray(new float[] { 70, 30 })).UseAllAvailableWidth();

            var invoiceTitle = new Paragraph($"Invoice {invoice.InvoiceNumber}")
                .SetFont(boldFont)
                .SetFontSize(20)
                .SetFontColor(black);
            headerTable.AddCell(new Cell().Add(invoiceTitle).SetBorder(Border.NO_BORDER));

            try
            {
                var rightCell = new Cell().SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT);
                rightCell.Add(new Paragraph("QuickHire")
                    .SetFont(boldFont)
                    .SetFontSize(14)
                    .SetFontColor(black)
                    .SetTextAlignment(TextAlignment.RIGHT));

                headerTable.AddCell(rightCell);
            }
            catch
            {
                headerTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
            }

            document.Add(headerTable);
            document.Add(new Paragraph("\n"));

            var detailsTable = new Table(UnitValue.CreatePercentArray(new float[] { 50, 50 })).UseAllAvailableWidth();

            var buyerInfo = new Paragraph()
                .Add(new Text("To:\n").SetFont(boldFont))
                .Add($"{invoice.BuyerName}\n")
                .Add($"Address: {invoice.BuyerAddress}\n")
                .Add($"Company name: {invoice.BuyerCompanyName}\n")
                .SetFont(regularFont);
            detailsTable.AddCell(new Cell().Add(buyerInfo).SetBorder(Border.NO_BORDER));

            var issueInfo = new Paragraph()
                .Add(new Text("Issued on:\n").SetFont(boldFont))
                .Add($"{invoice.CreatedAt:dd MMM, yyyy}\n\n")
                .Add(new Text("Order №:\n").SetFont(boldFont))
                .Add($"{invoice.OrderNumber}")
                .SetFont(regularFont);
            detailsTable.AddCell(new Cell().Add(issueInfo).SetBorder(Border.NO_BORDER));

            document.Add(detailsTable);
            document.Add(new Paragraph("\n"));

            var itemTable = new Table(UnitValue.CreatePercentArray(new float[] { 50, 25, 25 })).UseAllAvailableWidth();

            itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Service").SetFont(boldFont)));
            itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Quantity").SetFont(boldFont)));
            itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Total").SetFont(boldFont)));

            foreach (var item in invoice.Items)
            {
                itemTable.AddCell(new Cell().Add(new Paragraph(item.ItemName).SetFont(regularFont)));
                itemTable.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToString()).SetFont(regularFont)));
                itemTable.AddCell(new Cell().Add(new Paragraph($"${item.TotalAmount:F2}").SetFont(regularFont)));
            }

            document.Add(itemTable);
            document.Add(new Paragraph("\n"));

            var summaryTable = new Table(UnitValue.CreatePercentArray(new float[] { 75, 25 })).UseAllAvailableWidth();

            summaryTable.AddCell(new Cell()
                .Add(new Paragraph("Subtotal:").SetFont(boldFont))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT));
            summaryTable.AddCell(new Cell()
                .Add(new Paragraph($"${invoice.SubTotal:F2}").SetFont(regularFont))
                .SetBorder(Border.NO_BORDER));

            summaryTable.AddCell(new Cell()
                .Add(new Paragraph("Tax (15%):").SetFont(boldFont))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT));
            summaryTable.AddCell(new Cell()
                .Add(new Paragraph($"${invoice.Tax:F2}").SetFont(regularFont))
                .SetBorder(Border.NO_BORDER));

            summaryTable.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));
            summaryTable.AddCell(new Cell().Add(new Paragraph("")).SetBorder(Border.NO_BORDER));

            summaryTable.AddCell(new Cell()
                .Add(new Paragraph("Total:").SetFont(boldFont))
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT));
            summaryTable.AddCell(new Cell()
                .Add(new Paragraph($"${invoice.TotalAmount:F2}").SetFont(regularFont))
                .SetBorder(Border.NO_BORDER));

            document.Add(summaryTable);
        }

        var pdfBytes = memoryStream.ToArray();
        return new MemoryStream(pdfBytes);
    }

    public IFormFile GeneratePdfFromHtml(InvoiceModel invoice)
    {
        var stream = GeneratePdfStream(invoice);

        var formFile = new FormFile(stream, 0, stream.Length, "file", $"INV_{invoice.InvoiceNumber}.pdf")
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/pdf"
        };

        return formFile;
    }
}
