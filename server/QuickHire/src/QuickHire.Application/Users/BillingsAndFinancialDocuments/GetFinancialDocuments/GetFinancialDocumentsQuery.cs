using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.BillingsAndFinancialDocuments;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace QuickHire.Application.Users.BillingsAndFinancialDocuments.GetFinancialDocuments;

public record GetFinancialDocumentsQuery(bool Buyer, string? Keyword, string? FromDate, string? ToDate, int? OrderId) : IQuery<IEnumerable<FinancialDocumentRowModel>>;
