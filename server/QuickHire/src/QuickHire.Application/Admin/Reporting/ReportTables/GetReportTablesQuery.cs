using QuickHire.Application.Admin.Models.Report;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Reporting.ReportTables;

public record GetReportTablesQuery(int? GigId, string? UserId) : IQuery<ModerationStatusResponseModel>;

