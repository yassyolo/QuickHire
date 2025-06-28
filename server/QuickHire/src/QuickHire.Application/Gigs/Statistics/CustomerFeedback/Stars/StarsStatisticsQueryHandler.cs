using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;
using System.Globalization;

namespace QuickHire.Application.Gigs.Statistics.CustomerFeedback.Stars;

public class StarsStatisticsQueryHandler : IQueryHandler<StarsStatisticsQuery, StatisticsLineChartModel>
{
    private readonly IRepository _repository;

    public StarsStatisticsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<StatisticsLineChartModel> Handle(StarsStatisticsQuery request, CancellationToken cancellationToken)
    {
        var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var reviewsQuery = _repository.GetAllReadOnly<Review>()
            .Where(x => x.Order != null && x.Order.GigId == request.Id);

        var reviewsList = await _repository.ToListAsync(reviewsQuery);

        var fiveStarReviews = reviewsList.Where(x => x.Rating == 5).ToList();

        if (!fiveStarReviews.Any())
        {
            return new StatisticsLineChartModel
            {
                TotalItem = new TotalItemModel { Label = "5-Star Reviews", Count = "0" },
                PeakItem = new PeakModel { Date = "-" },
                ThisMonthItem = new ThisMonthModel { Count = "0", Percentage = "0" },
                Data = new List<LineChartDataPointModel>()
            };
        }

        var totalItem = new TotalItemModel
        {
            Label = "5-Star Reviews",
            Count = fiveStarReviews.Count.ToString(),
        };

        var peakStars = fiveStarReviews
            .GroupBy(x => x.CreatedOn.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .OrderByDescending(g => g.Count)
            .FirstOrDefault();

        var peak = new PeakModel
        {
            Date = peakStars?.Date.ToString("dd MMM") ?? "-"
        };

        var now = DateTime.Now;
        var lastMonth = now.AddMonths(-1);

        var thisMonthStars = fiveStarReviews.Count(x => x.CreatedOn.Month == now.Month && x.CreatedOn.Year == now.Year);
        var lastMonthStars = fiveStarReviews.Count(x => x.CreatedOn.Month == lastMonth.Month && x.CreatedOn.Year == lastMonth.Year);

        var percentageChange = lastMonthStars == 0 ? 0 : (thisMonthStars - lastMonthStars) * 100 / lastMonthStars;

        var thisMonthItem = new ThisMonthModel
        {
            Count = thisMonthStars.ToString(),
            Percentage = percentageChange.ToString()
        };

        var statistics = fiveStarReviews
            .GroupBy(x => new { x.CreatedOn.Year, x.CreatedOn.Month })
            .Select(g => new
            {
                Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                Count = g.Count()
            })
            .OrderBy(x => x.Date)
            .Select(x => new LineChartDataPointModel
            {
                Month = x.Date.ToString("MMM yyyy", CultureInfo.InvariantCulture),
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
