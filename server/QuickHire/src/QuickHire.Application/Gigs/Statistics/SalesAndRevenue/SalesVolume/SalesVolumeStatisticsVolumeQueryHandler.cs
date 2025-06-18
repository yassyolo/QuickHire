using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;
using System.Runtime.CompilerServices;

namespace QuickHire.Application.Gigs.Statistics.SalesAndRevenue.SalesVolume;

public class SalesVolumeStatisticsVolumeQueryHandler : IQueryHandler<SalesVolumeStatisticsVolumeQuery, StatisticsLineChartModel>
{
    private readonly IRepository _repository;

    public SalesVolumeStatisticsVolumeQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<StatisticsLineChartModel> Handle(SalesVolumeStatisticsVolumeQuery request, CancellationToken cancellationToken)
    {
       var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var sales = _repository.GetAllReadOnly<Order>().Where(x => x.GigId == request.Id);

        var salesList = await _repository.ToListAsync(sales);
        var totalItem = new TotalItemModel
        {
            Label = "Sales Volume",
            Count = salesList.Count().ToString(),
        };

        var peakSales = salesList.GroupBy(x => x.CreatedAt.Date)
            .Select(x => new
            {
                Date = x.Key,
                Count = x.Count()
            })
            .OrderByDescending(x => x.Count);

        var peak = new PeakModel
        {
            Date = peakSales.Select(x => x.Date).FirstOrDefault().ToString("dd MMM")
        };

        var thisMonthSales = salesList.Where(x => x.CreatedAt.Month == DateTime.Now.Month && x.CreatedAt.Year == DateTime.Now.Year).Count();
        var lastMonthSales = salesList.Where(x => x.CreatedAt.Month == DateTime.Now.AddMonths(-1).Month && x.CreatedAt.Year == DateTime.Now.AddMonths(-1).Year).Count();
        var thisMonth = new ThisMonthModel
        {
            Count = thisMonthSales.ToString(),
            Percentage = (lastMonthSales == 0 ? 0 : (thisMonthSales - lastMonthSales) * 100 / lastMonthSales).ToString(),
        };

        var statistics = salesList.GroupBy(x => x.CreatedAt.Month)
                        .Select(x => new LineChartDataPointModel
                        {
                            Month = x.Key.ToString("MMMM"),
                            Value = x.Count().ToString()
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
