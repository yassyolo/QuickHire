using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;
using System.Reflection.Metadata.Ecma335;

namespace QuickHire.Application.Users.Seller.Statistics.GigPerformance.Cards;

public class GetGigPerformanceCardsQueryHandler : IQueryHandler<GetGigPerformanceCardsQuery, IEnumerable<CardItemModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetGigPerformanceCardsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }   

    public async Task<IEnumerable<CardItemModel>> Handle(GetGigPerformanceCardsQuery request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var gigsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Gigs.Gig>().Where(x => x.SellerId == sellerId);
        var gigList = await _repository.ToListAsync<QuickHire.Domain.Gigs.Gig>(gigsQueryable);
        var gigIds = gigList.Select(x => x.Id).ToList();

        var ordersQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Orders.Order>();
        ordersQueryable = ordersQueryable.Where(x =>gigIds.Contains(x.GigId.Value));
        var orders = await _repository.ToListAsync<QuickHire.Domain.Orders.Order>(ordersQueryable);

        var favouriteGigsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.FavouriteGig>();
        favouriteGigsQueryable = favouriteGigsQueryable.Where(x => gigIds.Contains(x.GigId));
        var favouriteGigs = await _repository.ToListAsync<QuickHire.Domain.Users.FavouriteGig>(favouriteGigsQueryable);

        var gigsSold = orders.Count();
        var reviewsQueryable = _repository.GetAllIncluding<QuickHire.Domain.Orders.Review>(x => x.Order);
        reviewsQueryable = reviewsQueryable.Where(x => gigIds.Contains(x.Order.GigId.Value));
        var reviews = await _repository.ToListAsync<QuickHire.Domain.Orders.Review>(reviewsQueryable);

        var browsingHistoryQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.BrowsingHistory>();
        browsingHistoryQueryable = browsingHistoryQueryable!.Where(x => gigIds.Contains(x.GigId!.Value));
        var browsingHistory = await _repository.ToListAsync<QuickHire.Domain.Users.BrowsingHistory>(browsingHistoryQueryable);
        var repeatViewsCount = browsingHistory
            .GroupBy(x => x.GigId)
            .Select(x => new
            {
                GigId = x.Key,
                Count = x.Count()
            })
            .ToDictionary(x => x.GigId, x => x.Count);

        return new List<CardItemModel>
        {
            new CardItemModel
            {
                Title = "Gigs sold",
                Value = gigsSold.ToString()
            },
            new CardItemModel
            {
                Title = "Gig likes",
                Value = favouriteGigs.Count().ToString()
            },
            new CardItemModel
            {
                Title = "Repeat views",
                Value = repeatViewsCount.Values.Sum().ToString()
            },
            new CardItemModel
            {
                Title = "Reviews count",
                Value = reviews.Count().ToString()
            }
        };
    }
}

