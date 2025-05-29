using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;

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
        /*var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }*/

        var reviews = _repository.GetAllReadOnly<Review>().Where(x => x.Order.GigId == request.Id);
        var reviewsList = await _repository.ToListAsync(reviews);

        var totalItem = new TotalItemModel
        {
            Label = "Reviews",
            Count = reviewsList.Count().ToString(),
        };

        var peakReviews = reviewsList.GroupBy(x => x.CreatedOn.Date)
            .Select(x => new
            {
                Date = x.Key,
                Count = x.Count()
            })
            .OrderByDescending(x => x.Count);

        var peak = new PeakModel
        {
            Date = peakReviews.Select(x => x.Date).FirstOrDefault().ToString("dd MMM")
        };

        var thisMonthReviews = reviewsList.Where(x => x.CreatedOn.Month == DateTime.Now.Month && x.CreatedOn.Year == DateTime.Now.Year).Count();
        var lastMonthReviews = reviewsList.Where(x => x.CreatedOn.Month == DateTime.Now.AddMonths(-1).Month && x.CreatedOn.Year == DateTime.Now.AddMonths(-1).Year).Count();

        var thisMonth = new ThisMonthModel
        {
            Count = thisMonthReviews.ToString(),
            Percentage = (lastMonthReviews == 0 ? 0 : (thisMonthReviews - lastMonthReviews) * 100 / lastMonthReviews).ToString(),
        };

        var statistics = reviewsList.GroupBy(x => x.CreatedOn.Month)
                        .Select(x => new LineChartDataPointModel
                        {
                            Month = x.Key.ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture),
                            Value = x.Count().ToString()
                        })
                        .OrderBy(x => x.Month)
                        .ToList();

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
                Label = "5 * rating",
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

