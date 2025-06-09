using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Reporting.Report;

public record ReportItemCommand(int? GigId, int? SellerId, string Reason) : ICommand<Unit>;

