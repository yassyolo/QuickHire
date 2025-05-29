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

        var orders = _repository.GetAllReadOnly<Order>().Where(x => x.GigId == request.Id);
        var ordersList = await _repository.ToListAsync(orders);
        var revenue = ordersList.Sum(x => x.TotalPrice);

        var totalItem = new TotalItemModel
        {
            Label = "Average Order Value",
            Count = revenue.ToString("C"),
        };

        var peakAverageOrderValue = ordersList.GroupBy(x => x.CreatedAt.Date)
            .Select(x => new
            {
                Date = x.Key,
                AverageOrderValue = x.Sum(y => y.TotalPrice) / x.Count()
            })
            .OrderByDescending(x => x.AverageOrderValue);

        var peak = new PeakModel
        {
            Date = peakAverageOrderValue.Select(x => x.Date).FirstOrDefault().ToString("dd MMM")
        };

        var thisMonthAverageOrderValue = ordersList.Where(x => x.CreatedAt.Month == DateTime.Now.Month && x.CreatedAt.Year == DateTime.Now.Year).Sum(x => x.TotalPrice);
        var lastMonthAverageOrderValue = ordersList.Where(x => x.CreatedAt.Month == DateTime.Now.AddMonths(-1).Month && x.CreatedAt.Year == DateTime.Now.AddMonths(-1).Year).Sum(x => x.TotalPrice);

        var thisMonth = new ThisMonthModel
        {
            Count = thisMonthAverageOrderValue.ToString("C"),
            Percentage = (lastMonthAverageOrderValue == 0 ? 0 : (thisMonthAverageOrderValue - lastMonthAverageOrderValue) * 100 / lastMonthAverageOrderValue).ToString("P"),
        };

        var statistics = ordersList.GroupBy(x => x.CreatedAt.Month)
            .Select(x => new LineChartDataPointModel
            {
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(x.Key),
                Value = (x.Sum(y => y.TotalPrice) / x.Count()).ToString("C")
            });
        /*return new StatisticsLineChartModel
        {
            TotalItem = totalItem,
            PeakItem = peak,
            ThisMonthItem = thisMonth,
            Data = statistics,
        };*/

        return new StatisticsLineChartModel
        {
            TotalItem = new TotalItemModel
            {
                Label = "Likes",
                Count = "1234"
            },
            PeakItem = new PeakModel
            {
                Date = "12 Mar"
            },
            ThisMonthItem = new ThisMonthModel
            {
                Count = "234",
                Percentage = "25"
            },
            Data = new List<LineChartDataPointModel>
            {
                new LineChartDataPointModel { Month = "January", Value = "150" },
                new LineChartDataPointModel { Month = "February", Value = "180" },
                new LineChartDataPointModel { Month = "March", Value = "210" },
                new LineChartDataPointModel { Month = "April", Value = "220" },
                new LineChartDataPointModel { Month = "May", Value = "234" },
            }
        };
    }
}
