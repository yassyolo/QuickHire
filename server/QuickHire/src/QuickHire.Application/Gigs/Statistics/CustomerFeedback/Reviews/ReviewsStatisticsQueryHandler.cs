using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;
using System.Globalization;

namespace QuickHire.Application.Gigs.Statistics.CustomerFeedback.Reviews;

public class ReviewsStatisticsQueryHandler : IQueryHandler<ReviewsStatisticsQuery, StatisticsLineChartModel>
{
    private readonly IRepository _repository;

    public ReviewsStatisticsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<StatisticsLineChartModel> Handle(ReviewsStatisticsQuery request, CancellationToken cancellationToken)
    {
        var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var reviews = _repository.GetAllReadOnly<Review>().Where(x => x.Order.GigId == request.Id);
        var reviewsList = await _repository.ToListAsync(reviews);

        var totalItem = new TotalItemModel
        {
            Label = "Reviews",
            Count = reviewsList.Count().ToString(),
        };

        var peakReviews = reviewsList.GroupBy(x => new { x.CreatedOn.Year, x.CreatedOn.Month })
            .Select(x => new
            {
                Date = new DateTime(x.Key.Year, x.Key.Month, 1),
                Average = x.Average(r => r.Rating)
            })
            .OrderByDescending(x => x.Average)
            .FirstOrDefault();

        var peak = new PeakModel
        {
            Date = peakReviews?.Date.ToString("MMM yyyy") ?? "-"
        };

        var now = DateTime.Now;
        var thisMonthReviews = reviewsList.Count(x => x.CreatedOn.Month == now.Month && x.CreatedOn.Year == now.Year);

        var lastMonth = now.AddMonths(-1);
        var lastMonthReviews = reviewsList.Count(x => x.CreatedOn.Month == lastMonth.Month && x.CreatedOn.Year == lastMonth.Year);

        var percentageChange = lastMonthReviews == 0 ? 0 : (thisMonthReviews - lastMonthReviews) * 100 / lastMonthReviews;

        var thisMonthItem = new ThisMonthModel
        {
            Count = thisMonthReviews.ToString(),
            Percentage = percentageChange.ToString()
        };

        var statistics = reviewsList
            .GroupBy(x => new { x.CreatedOn.Year, x.CreatedOn.Month })
            .Select(g => new LineChartDataPointModel
            {
                Month = new DateTime(g.Key.Year, g.Key.Month, 1)
                            .ToString("MMM yyyy", CultureInfo.InvariantCulture),
                Value = (g.Sum(r => r.Rating) / (double)g.Count()).ToString("0.0") 
            })
            .OrderBy(dp => DateTime.ParseExact(dp.Month, "MMM yyyy", CultureInfo.InvariantCulture))
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
