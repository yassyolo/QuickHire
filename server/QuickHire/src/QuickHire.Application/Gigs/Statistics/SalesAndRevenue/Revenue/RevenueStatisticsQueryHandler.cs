using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Gigs.Statistics.SalesAndRevenue.Revenue;

public class RevenueStatisticsQueryHandler : IQueryHandler<RevenueStatisticsQuery, StatisticsLineChartModel>
{
    private readonly IRepository _repository;

    public RevenueStatisticsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<StatisticsLineChartModel> Handle(RevenueStatisticsQuery request, CancellationToken cancellationToken)
    {
        var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var orders = _repository.GetAllReadOnly<Order>().Where(x => x.GigId == request.Id);
        var ordersList = await _repository.ToListAsync(orders);

        var totalItem = new TotalItemModel
        {
            Label = "Revenue",
            Count = ordersList.Sum(x => x.TotalPrice).ToString("C", System.Globalization.CultureInfo.CurrentCulture),
        };

        var peakRevenue = ordersList.GroupBy(x => x.CreatedAt.Date)
            .Select(x => new
            {
                Date = x.Key,
                Count = x.Sum(y => y.TotalPrice)
            })
            .OrderByDescending(x => x.Count);

        var peak = new PeakModel
        {
            Date = peakRevenue.Select(x => x.Date).FirstOrDefault().ToString("dd MMM")
        };

        var thisMonthRevenue = ordersList.Where(x => x.CreatedAt.Month == DateTime.Now.Month && x.CreatedAt.Year == DateTime.Now.Year).Sum(x => x.TotalPrice);
        var lastMonthRevenue = ordersList.Where(x => x.CreatedAt.Month == DateTime.Now.AddMonths(-1).Month && x.CreatedAt.Year == DateTime.Now.AddMonths(-1).Year).Sum(x => x.TotalPrice);
        var thisMonth = new ThisMonthModel
        {
            Count = thisMonthRevenue.ToString("C", System.Globalization.CultureInfo.CurrentCulture),
            Percentage = (lastMonthRevenue == 0 ? 0 : (thisMonthRevenue - lastMonthRevenue) * 100 / lastMonthRevenue).ToString("F2") + "%",
        };

        var statistics = ordersList.GroupBy(x => x.CreatedAt.Month)
                        .Select(x => new LineChartDataPointModel
                        {
                            Month = x.Key.ToString("MMMM"),
                            Value = x.Sum(y => y.TotalPrice).ToString("C", System.Globalization.CultureInfo.CurrentCulture)
                        })
                        .OrderBy(x => x.Month)
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

