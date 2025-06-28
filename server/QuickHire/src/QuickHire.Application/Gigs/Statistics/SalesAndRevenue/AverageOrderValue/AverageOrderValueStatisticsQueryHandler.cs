using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;
using System.Globalization;

namespace QuickHire.Application.Gigs.Statistics.SalesAndRevenue.AverageOrderValue;

public class AverageOrderValueStatisticsQueryHandler : IQueryHandler<AverageOrderValueStatisticsQuery, StatisticsLineChartModel>
{
    private readonly IRepository _repository;

    public AverageOrderValueStatisticsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

public async Task<StatisticsLineChartModel> Handle(AverageOrderValueStatisticsQuery request, CancellationToken cancellationToken)
{
    var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
    if (gig == null)
    {
        throw new NotFoundException(nameof(Gig), request.Id);
    }

    var ordersQuery = _repository.GetAllReadOnly<Order>().Where(x => x.GigId == request.Id);
    var ordersList = await _repository.ToListAsync(ordersQuery);

    var revenue = ordersList.Sum(x => x.TotalPrice);

    var totalItem = new TotalItemModel
    {
        Label = "Average Order Value",
        Count = revenue.ToString("C"),  
    };

    var peakAverageOrderValue = ordersList
        .GroupBy(x => x.CreatedAt.Date)
        .Select(g => new
        {
            Date = g.Key,
            AverageOrderValue = g.Average(y => y.TotalPrice)
        })
        .OrderByDescending(x => x.AverageOrderValue)
        .FirstOrDefault();

    var peak = new PeakModel
    {
        Date = peakAverageOrderValue?.Date.ToString("dd MMM") ?? "-"
    };

    var now = DateTime.Now;
    var lastMonth = now.AddMonths(-1);

    var thisMonthOrders = ordersList.Where(x => x.CreatedAt.Year == now.Year && x.CreatedAt.Month == now.Month);
    var lastMonthOrders = ordersList.Where(x => x.CreatedAt.Year == lastMonth.Year && x.CreatedAt.Month == lastMonth.Month);

    var thisMonthAverage = thisMonthOrders.Any() ? thisMonthOrders.Average(x => x.TotalPrice) : 0;
    var lastMonthAverage = lastMonthOrders.Any() ? lastMonthOrders.Average(x => x.TotalPrice) : 0;

    double percentageChange = (double)(lastMonthAverage == 0
        ? 0
        : ((thisMonthAverage - lastMonthAverage) / lastMonthAverage));

    var thisMonth = new ThisMonthModel
    {
        Count = thisMonthAverage.ToString("C"),
        Percentage = percentageChange.ToString("P1")  
    };

    var statistics = ordersList
        .GroupBy(x => new { x.CreatedAt.Year, x.CreatedAt.Month })
        .OrderBy(g => g.Key.Year)
        .ThenBy(g => g.Key.Month)
        .Select(g => new LineChartDataPointModel
        {
            Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy", CultureInfo.InvariantCulture),
            Value = g.Average(y => y.TotalPrice).ToString("C")
        })
        .ToList();

    return new StatisticsLineChartModel
    {
        TotalItem = totalItem,
        PeakItem = peak,
        ThisMonthItem = thisMonth,
        Data = statistics,
    };
}

}
