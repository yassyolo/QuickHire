using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;
using System.Globalization;

namespace QuickHire.Application.Gigs.Statistics.Engagement.Likes;

public class LikesStatisticsQueryHandler : IQueryHandler<LikesStatisticsQuery, StatisticsLineChartModel>
{
    private readonly IRepository _repository;

    public LikesStatisticsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<StatisticsLineChartModel> Handle(LikesStatisticsQuery request, CancellationToken cancellationToken)
    {
        /*var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }*/

        var likes = _repository.GetAllReadOnly<FavouriteGig>().Where(x => x.GigId == request.Id);
        var likesList = await _repository.ToListAsync(likes);

        var totalItem = new TotalItemModel
        {
            Label = "Likes",
            Count = likesList.Count().ToString(),
        };

        var peakLikes = likesList.GroupBy(x => x.AddedAt.Date)
            .Select(x => new
            {
                Date = x.Key,
                Count = x.Count()
            })
            .OrderByDescending(x => x.Count);

        var peak = new PeakModel
        {
            Date = peakLikes.Select(x => x.Date).FirstOrDefault().ToString("dd MMM")
        };

        var thisMonthLikes = likesList.Where(x => x.AddedAt.Month == DateTime.Now.Month && x.AddedAt.Year == DateTime.Now.Year).Count();
        var lastMonthLikes = likesList.Where(x => x.AddedAt.Month == DateTime.Now.AddMonths(-1).Month && x.AddedAt.Year == DateTime.Now.AddMonths(-1).Year).Count();

        var thisMonth = new ThisMonthModel
        {
            Count = thisMonthLikes.ToString(),
            Percentage = (lastMonthLikes == 0 ? 0 : (thisMonthLikes - lastMonthLikes) * 100 / lastMonthLikes).ToString(),
        };

        var statistics = likesList.GroupBy(x => x.AddedAt.Month)
                        .Select(x => new LineChartDataPointModel
                        {
                            Month = x.Key.ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture),
                            Value = x.Count().ToString()
                        })
                        .OrderBy(x => DateTime.ParseExact(x.Month, "MMMM", CultureInfo.CurrentCulture))
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

