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
        var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var likesQuery = _repository.GetAllReadOnly<FavouriteGig>().Where(x => x.GigId == request.Id);
        var likesList = await _repository.ToListAsync(likesQuery);

        var totalItem = new TotalItemModel
        {
            Label = "Likes",
            Count = likesList.Count().ToString()
        };

        var peakLikes = likesList.GroupBy(x => x.AddedAt.Date)
            .Select(x => new
            {
                Date = x.Key,
                Count = x.Count()
            })
            .OrderByDescending(x => x.Count)
            .FirstOrDefault();

        var peak = new PeakModel
        {
            Date = peakLikes?.Date.ToString("dd MMM") ?? "-"
        };

        var now = DateTime.Now;
        var thisMonthLikes = likesList.Count(x => x.AddedAt.Month == now.Month && x.AddedAt.Year == now.Year);

        var lastMonth = now.AddMonths(-1);
        var lastMonthLikes = likesList.Count(x => x.AddedAt.Month == lastMonth.Month && x.AddedAt.Year == lastMonth.Year);

        var percentageChange = lastMonthLikes == 0 ? 0 : (thisMonthLikes - lastMonthLikes) * 100 / lastMonthLikes;

        var thisMonthItem = new ThisMonthModel
        {
            Count = thisMonthLikes.ToString(),
            Percentage = percentageChange.ToString()
        };

        var statistics = likesList
            .GroupBy(x => new { x.AddedAt.Year, x.AddedAt.Month })
            .Select(x => new
            {
                Year = x.Key.Year,
                MonthNumber = x.Key.Month,
                MonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.Key.Month),
                Count = x.Count()
            })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.MonthNumber)
            .Select(x => new LineChartDataPointModel
            {
                Month = x.MonthName,
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
