using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;
using System.Globalization;

namespace QuickHire.Application.Gigs.Statistics.Engagement.Views;

public class ViewStatisticsQueryHandler : IQueryHandler<ViewStatisticsQuery, StatisticsLineChartModel>
{
    private readonly IRepository _repository;

    public ViewStatisticsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<StatisticsLineChartModel> Handle(ViewStatisticsQuery request, CancellationToken cancellationToken)
    {
        var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var browsingHistoryQuery = _repository.GetAllReadOnly<QuickHire.Domain.Users.BrowsingHistory>().Where(x => x.GigId == request.Id);
        var browsingHistoryList = await _repository.ToListAsync(browsingHistoryQuery);

        var totalItem = new TotalItemModel
        {
            Label = "Views",
            Count = browsingHistoryList.Count().ToString()
        };

        var peakViews = browsingHistoryList.GroupBy(x => x.ViewedAt.Date)
            .Select(x => new
            {
                Date = x.Key,
                Count = x.Count()
            })
            .OrderByDescending(x => x.Count)
            .FirstOrDefault();

        var peak = new PeakModel
        {
            Date = peakViews?.Date.ToString("dd MMM") ?? "-"
        };

        var now = DateTime.Now;
        var thisMonthViews = browsingHistoryList.Count(x => x.ViewedAt.Month == now.Month && x.ViewedAt.Year == now.Year);

        var lastMonth = now.AddMonths(-1);
        var lastMonthViews = browsingHistoryList.Count(x => x.ViewedAt.Month == lastMonth.Month && x.ViewedAt.Year == lastMonth.Year);

        var percentageChange = lastMonthViews == 0 ? 0 : (thisMonthViews - lastMonthViews) * 100 / lastMonthViews;

        var thisMonthItem = new ThisMonthModel
        {
            Count = thisMonthViews.ToString(),
            Percentage = percentageChange.ToString()
        };

        var statistics = browsingHistoryList
            .GroupBy(x => new { x.ViewedAt.Year, x.ViewedAt.Month })
            .Select(x => new
            {
                Year = x.Key.Year,
                MonthNumber = x.Key.Month,
                MonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.Key.Month),
                Count = x.Count()
            })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.MonthNumber)
            .Select(x => new LineChartDataPointModel
            {
                Month = x.MonthName,
                Value = x.Count.ToString()
            })
            .ToList();

        return new StatisticsLineChartModel
        {
            TotalItem = totalItem,
            PeakItem = peak,
            ThisMonthItem = thisMonthItem,
            Data = statistics
        };
    }
}
