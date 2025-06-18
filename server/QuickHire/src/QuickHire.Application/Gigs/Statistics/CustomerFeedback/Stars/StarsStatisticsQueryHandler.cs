using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;

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

        var reviews = _repository.GetAllReadOnly<Review>().Where(x => x.Order.GigId == request.Id);
        var reviewsList = await _repository.ToListAsync(reviews);

        var totalItem = new TotalItemModel
        {
            Label = "Stars",
            Count = reviewsList.Count(x => x.Rating == 5).ToString(),
        };

        var peakStars = reviewsList.Where(x => x.Rating == 5).GroupBy(x => x.CreatedOn.Date)
            .Select(x => new
            {
                Date = x.Key,
                Count = x.Count()
            })
            .OrderByDescending(x => x.Count);

        var peak = new PeakModel
        {
            Date = peakStars.Select(x => x.Date).FirstOrDefault().ToString("dd MMM")
        };

        var thisMonthStars = reviewsList.Where(x => x.CreatedOn.Month == DateTime.Now.Month && x.CreatedOn.Year == DateTime.Now.Year && x.Rating == 5).Count();
        var lastMonthStars = reviewsList.Where(x => x.CreatedOn.Month == DateTime.Now.AddMonths(-1).Month && x.CreatedOn.Year == DateTime.Now.AddMonths(-1).Year && x.Rating == 5).Count();
        var thisMonth = new ThisMonthModel
        {
            Count = thisMonthStars.ToString(),
            Percentage = (lastMonthStars == 0 ? 0 : (thisMonthStars - lastMonthStars) * 100 / lastMonthStars).ToString(),
        };

        var statistics = reviewsList.GroupBy(x => x.CreatedOn.Month)
            .Select(x => new LineChartDataPointModel
            {
                Month = x.Key.ToString(),
                Value = x.Count(x => x.Rating == 5).ToString()
            })
            .OrderBy(x => x.Month);

       return new StatisticsLineChartModel
       {
           TotalItem = totalItem,
           PeakItem = peak,
           ThisMonthItem = thisMonth,
           Data = statistics,
       };
    }
}


