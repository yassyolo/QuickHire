using Microsoft.AspNetCore.Http;
using QuickHire.Domain.Orders;

namespace QuickHire.Infrastructure.Helpers;

public interface IPdfHelper
{
    IFormFile GeneratePdfFromHtml(Invoice invoice);
}
