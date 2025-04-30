using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using QuickHire.Domain.Orders;
using QuickHire.Infrastructure.Helpers.Models;

namespace QuickHire.Infrastructure.Helpers;

internal class PdfHelper : IPdfHelper
{
    private readonly IConverter _converter;

    public PdfHelper()
    {
        _converter = new BasicConverter(new PdfTools());
    }

    public IFormFile GeneratePdfFromHtml(Invoice invoice)
    {
        var invoiceModel = new InvoiceModel()
        {
            InvoiceNumber = invoice.InvoiceNumber,
            BuyerName = invoice.Buyer.BillingDetails.FullName,
            BuyerAddress = invoice.Buyer.BillingDetails.Address.,
            BuyerCompanyName = invoice.Buyer.BillingDetails.CompanyName,
            CreatedAt = invoice.CreatedAt.ToString(),
            OrderNumber = invoice.Order.OrderNumber,
            TotalAmount = invoice.TotalAmount.ToString(),
            SubTotal = invoice.Subtotal.ToString(),
            Tax = invoice.Tax.ToString(),
            Items = new List<InvoiceItemModel>
            {
                new()
                {
                    ItemName = invoice.Order.Gig.Title,
                    Quantity = 1,
                    TotalAmount = invoice.Order.Gig.Price.ToString()
                },
                new()
                {
                    ItemName = "Service fee",
                    Quantity = 1,
                    TotalAmount = invoice.ServiceFee.ToString()
                }
            }
        };
      
    
         var htmlContent = GenerateInvoiceHtml(invoiceModel);

        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {ColorMode = ColorMode.Color,
                              Orientation = Orientation.Portrait,
                              PaperSize = PaperKind.A4},
            Objects = {new ObjectSettings() 
                      {HtmlContent = htmlContent,
                       WebSettings = new WebSettings() {LoadImages = true,
                                                        PrintMediaType = true}}}
        };

        using (var memoryStream = new MemoryStream())
        {
            _converter.Convert(doc); 

            memoryStream.Position = 0;

            var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", $"INV_{invoice.InvoiceNumber}.pdf")
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };

            return formFile;
        }
    }

    public string GenerateInvoiceHtml(InvoiceModel invoice)
    {
        string html = $@"
                           <!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Invoice</title>
    <style>
        body {{
            font-family: 'Macan', sans-serif;
            margin: 20px;
        }}
        .invoice-container {{
            width: 100%;
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 10px;
            background-color: #ffffff;
        }}
        .invoice-header {{
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 70px;
        }}
        .invoice-info h2 {{
            margin: 0;
            color: #222325;
        }}
        .logo {{
            text-align: center;
            display: flex;
            align-items: center;
            gap: 10px;
        }}
        .logo img {{
            width: 80px;
        }}
        .invoice-details {{
            margin-top: 20px;
            margin-bottom: 30px;
            display: flex;
            flex-direction: row;
            justify-content: space-between;
        }}
        .invoice-details p {{
            margin: 5px 0;
        }}
        .invoice-table {{
            width: 100%;
            margin-top: 30px;
            border-collapse: collapse;
        }}
        .invoice-table th, .invoice-table td {{
            padding: 10px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }}
        .invoice-table th {{
            background-color: #f2f2f2;
        }}
        .invoice-table td {{
            border: none;
        }}
        .invoice-summary {{
            display: flex;
            justify-content: flex-end;
            margin-top: 30px;
        }}
        .invoice-summary .total {{
            font-weight: bold;
            font-size: 16px;
        }}
        .invoice-info {{
            display: flex;
            flex-direction: column;
            gap: 5px;
        }}
        thead, tbody {{
            border-bottom: 1px solid #000;
        }}
        strong {{
            color: #222325;
        }}
        tr {{
            margin-bottom: 10px;
        }}
    </style>
</head>
<body>

<div class='invoice-container'>
    <div class='invoice-header'>
        <div class='invoice-info'>
            <h2>Invoice {invoice.InvoiceNumber}</h2>
        </div>
        <div class='logo'>
            <img src='your-logo.png' alt='Logo'/>
            <h3>QuickHire</h3>
        </div>
    </div>

    <div class='invoice-details'>
        <div class='invoice-info'>
            <p><strong>To:</strong> {invoice.BuyerName}</p>
            <p><strong>Address:</strong> {invoice.BuyerAddress}</p>
            <p><strong>Company name:</strong> {invoice.BuyerCompanyName}</p>
        </div>
        <div class='invoice-info'>
            <p><strong>Issued on:</strong> {invoice.CreatedAt:yyyy-MM-dd}</p>
            <p><strong>Order &numero;:</strong> {invoice.OrderNumber}</p>
        </div>
    </div>

    <table class='invoice-table'>
        <thead>
            <tr>
                <th>Service</th>
                <th>Quantity</th>
                <th>Total</th>
            </tr>
        </thead>
        <tbody>";

        foreach (var item in invoice.Items)
        {
            html += $@"
                <tr>
                    <td>{item.ItemName}</td>
                    <td>{item.Quantity}</td>
                    <td>${item.TotalAmount:F2}</td>
                </tr>";
        }

        html += $@"
        </tbody>
    </table>

    <div class='invoice-summary'>
        <div class='total'>
            <p><strong>Subtotal:</strong> ${invoice.SubTotal}</p>
            <p><strong>Tax (10%):</strong> ${invoice.Tax}</p>
            <div style='border-bottom: 1px solid #000; width: 100%; margin: 10px 0;'></div>
            <p><strong>Total:</strong> ${invoice.TotalAmount}</p>
        </div>
    </div>
</div>

</body>
</html>";

        return html;
    }
}
