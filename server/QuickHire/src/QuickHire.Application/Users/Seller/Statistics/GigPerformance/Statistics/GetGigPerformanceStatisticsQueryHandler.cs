using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Seller.Statistics.GigPerformance.Statistics;

public class GetGigPerformanceStatisticsQueryHandler : IQueryHandler<GetGigPerformanceStatisticsQuery, IEnumerable<GigPerformanceRowModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetGigPerformanceStatisticsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<GigPerformanceRowModel>> Handle(GetGigPerformanceStatisticsQuery request, CancellationToken cancellationToken)
    {
        var (startDate, endDate) = ParseRange(request.Range ?? "last 30 days");
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var gigsQueryable = _repository.GetAllReadOnly<Gig>().Where(x => x.SellerId == sellerId);
        var gigs = await _repository.ToListAsync(gigsQueryable);
        var gigIds = gigs.Select(x => x.Id).ToList();

        var ordersQueryable = _repository.GetAllReadOnly<Order>().Where(x => x.GigId.HasValue && gigIds.Contains(x.GigId.Value) && x.CreatedAt.Date >= startDate && x.CreatedAt.Date <= endDate);
        var orders = await _repository.ToListAsync(ordersQueryable);

        var favouritesQueryable = _repository.GetAllReadOnly<FavouriteGig>().Where(x => gigIds.Contains(x.GigId) && x.AddedAt.Date >= startDate && x.AddedAt.Date <= endDate);
        var favourites = await _repository.ToListAsync(favouritesQueryable);

        var browsingHistoryQueryable = _repository.GetAllReadOnly<BrowsingHistory>().Where(x => gigIds.Contains(x.GigId.Value) && x.ViewedAt.Date >= startDate && x.ViewedAt.Date <= endDate);
        var views = await _repository.ToListAsync(browsingHistoryQueryable);

        var reviewsQueryable = _repository.GetAllReadOnly<Review>().Where(x => x.Order.GigId.HasValue && gigIds.Contains(x.Order.GigId.Value) && x.CreatedOn.Date >= startDate && x.CreatedOn.Date <= endDate);
        var reviews = await _repository.ToListAsync(reviewsQueryable);

        var dateRange = Enumerable.Range(0, (endDate - startDate).Days + 1).Select(x => startDate.AddDays(x))
                                  .ToList();

        var result = dateRange.Select(date =>
        {
            var dateOnly = date.Date;

            var gigsSold = orders.Count(x => x.CreatedAt.Date == dateOnly);
            var likes = favourites.Count(x => x.AddedAt.Date == dateOnly);
            var repeatViews = views.Count(x => x.ViewedAt.Date == dateOnly);
            var comments = reviews.Count(x => x.CreatedOn.Date == dateOnly);

            return new GigPerformanceRowModel
            {
                Date = dateOnly.ToString("yyyy-MM-dd"),
                GigsSold = gigsSold,
                GigLikes = likes,
                GigFavourites = likes,
                RepeatViews = repeatViews,
                CommentsCount = comments
            };
        });

        return result;
    }

    private (DateTime Start, DateTime End) ParseRange(string range)
    {
        var today = DateTime.UtcNow.Date;

        return range.Trim().ToLower() switch
        {
            "last 30 days" => (today.AddDays(-29), today),
            "last 3 months" => (today.AddMonths(-3), today),
            "yearly" => (new DateTime(today.Year, 1, 1), today),
            _ => (today.AddDays(-29), today)
        };
    }
}

