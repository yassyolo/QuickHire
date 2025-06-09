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
        /*var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }*/

        var browsingHistory = _repository.GetAllReadOnly<QuickHire.Domain.Users.BrowsingHistory>().Where(x => x.GigId == request.Id);
        var browsingHistoryList = await _repository.ToListAsync(browsingHistory);

        var totalItem = new TotalItemModel
        {
            Label = "Views",
            Count = browsingHistoryList.Count().ToString(),
        };

        var peakViews = browsingHistoryList.GroupBy(x => x.ViewedAt.Date)
            .Select(x => new
            {
                Date = x.Key,
                Count = x.Count()
            })
            .OrderByDescending(x => x.Count);

        var peak = new PeakModel
        {
            Date = peakViews.Select(x => x.Date).FirstOrDefault().ToString("dd MMM")
        };

        var thisMonthViews = browsingHistoryList.Where(x => x.ViewedAt.Month == DateTime.Now.Month && x.ViewedAt.Year == DateTime.Now.Year).Count();
        var lastMonthViews = browsingHistoryList.Where(x => x.ViewedAt.Month == DateTime.Now.AddMonths(-1).Month && x.ViewedAt.Year == DateTime.Now.AddMonths(-1).Year).Count();

        var thisMonth = new ThisMonthModel
        {
            Count = thisMonthViews.ToString(),
            Percentage = (lastMonthViews == 0 ? 0 : (thisMonthViews - lastMonthViews) * 100 / lastMonthViews).ToString(),
        };

        var statistics = browsingHistoryList.GroupBy(x => x.ViewedAt.Month)
                        .Select(x => new LineChartDataPointModel
                        {
                            Month = x.Key.ToString("MMMM", CultureInfo.InvariantCulture),
                            Value = x.Count().ToString()
                        })
                        .OrderBy(x => DateTime.ParseExact(x.Month, "MMMM", CultureInfo.CurrentCulture))
                        .ToList();

        /*return new StatisticsLineChartModel
        {
            TotalItem = totalItem,
            PeakItem = peak,
            ThisMonthItem = thisMonth,
            Data = statistics         
        };*/

        return new StatisticsLineChartModel
        {
            TotalItem = new TotalItemModel
            {
                Label = "Views",
                Count = "1098"
            },
            PeakItem = new PeakModel
            {
                Date = "10 Apr"
            },
            ThisMonthItem = new ThisMonthModel
            {
                Count = "202",
                Percentage = "15"
            },
            Data = new List<LineChartDataPointModel>
        {
            new LineChartDataPointModel { Month = "January", Value = "150" },
            new LineChartDataPointModel { Month = "February", Value = "190" },
            new LineChartDataPointModel { Month = "March", Value = "220" },
            new LineChartDataPointModel { Month = "April", Value = "270" },
            new LineChartDataPointModel { Month = "May", Value = "268" },
        }
        };
    }
}

