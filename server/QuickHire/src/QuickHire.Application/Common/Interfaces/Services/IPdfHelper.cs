using Microsoft.AspNetCore.Http;
using QuickHire.Application.Orders.Models.Invoice;

namespace QuickHire.Infrastructure.Helpers;

public interface IPdfHelper
{
    IFormFile GeneratePdfFromHtml(InvoiceModel invoice);
}
