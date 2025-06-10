using QuickHire.Application.Admin.Models.Report;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Moderation;
using System.Text.RegularExpressions;

namespace QuickHire.Application.Admin.Reporting.ReportTables;

public class GetReportTablesQueryHandler : IQueryHandler<GetReportTablesQuery, ModerationStatusResponseModel>
{
    private readonly IUserService _userService;
    private readonly IRepository _repository;

    public GetReportTablesQueryHandler(IUserService userService, IRepository repository)
    {
        _userService = userService;
        _repository = repository;
    }
    public async Task<ModerationStatusResponseModel> Handle(GetReportTablesQuery request, CancellationToken cancellationToken)
    {
        var reportedItemsQueryable = _repository.GetAllReadOnly<ReportedItem>();

        if (request.GigId.HasValue)
        {
            reportedItemsQueryable = reportedItemsQueryable.Where(x => x.GigId == request.GigId.Value);
        }

        if (!string.IsNullOrEmpty(request.UserId))
        {
            reportedItemsQueryable = reportedItemsQueryable.Where(x => x.ReportedUserId == request.UserId);
        }

        var reportedItems = await _repository.ToListAsync(reportedItemsQueryable);

        var reportTable = await Task.WhenAll(
    reportedItems.Select(async x =>
    {
        var reportedByUserInfo = await _userService.GetUserEmailByUserIdAsync(x.ReportedById);
        return new ReportTableModel
        {
            Id = x.Id,
            CreatedBy = reportedByUserInfo,
            Reason = x.Reason,
            CreatedOn = x.CreatedAt.ToString("yyyy-MM-dd"),
        };
    }));

        var status = string.Empty;
        if(request.GigId.HasValue)
        {
            var gig = await _repository.GetByIdAsync<Domain.Gigs.Gig, int>(request.GigId.Value);
            status = gig?.ModerationStatus.ToString();
        }
        else if (!string.IsNullOrEmpty(request.UserId))
        {
            
            status = await _userService.GetUserModerationStatusAsync(request.UserId);
        }

        return new ModerationStatusResponseModel
        {
            ModerationStatus = reportTable.ToList(),
            Status = SplitPascalCase(status)
        };

    }

    private string SplitPascalCase(string input)
    {
        return Regex.Replace(input, @"(?<=[a-z])(?=[A-Z])", " ");
    }
}
