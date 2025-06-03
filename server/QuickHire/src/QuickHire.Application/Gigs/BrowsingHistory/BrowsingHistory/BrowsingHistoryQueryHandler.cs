using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Shared;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Gigs.BrowsingHistory.BrowsingHistory;

public class BrowsingHistoryQueryHandler : IQueryHandler<BrowsingHistoryQuery, IEnumerable<GigCardModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public BrowsingHistoryQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<GigCardModel>> Handle(BrowsingHistoryQuery request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();

        var favouriteGigsQueryable = _repository.GetAllReadOnly<FavouriteGig>().Where(x => x.BuyerId == buyerId);
        var favouriteGigsIdsList = await _repository.ToListAsync<FavouriteGig>(favouriteGigsQueryable);

        var browsingHistoryQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.BrowsingHistory>().Where(x => x.BuyerId == buyerId);
        browsingHistoryQueryable = _repository.GetAllIncluding<QuickHire.Domain.Users.BrowsingHistory>(x => x.Gig, x => x.Gig.Orders, x => x.Gig.PaymentPlans).OrderByDescending(x => x.ViewedAt);
        var browsingHistoryList = await _repository.ToListAsync(browsingHistoryQueryable);

        var result = new List<GigCardModel>();  

        foreach(var bh in browsingHistoryList)
        {
            var gig = bh.Gig;
            var gigSellerDetails = await _userService.GetSellerDetailsForGigCardByIdAsync(gig.SellerId);
            var gigReviews = gig.Orders.Select(x => x.Reviews).SelectMany(x => x).ToList();

            var gigCardModel = new GigCardModel
            {
                Id = gig.Id,
                Title = gig.Title,
                FromPrice = gig.PaymentPlans.Any() ? gig.PaymentPlans.Min(x => x.Price) : 0,
                ImageUrls = gig.ImageUrls,
                SellerName = gigSellerDetails.name,
                SellerId = gig.SellerId,
                SellerProfileImageUrl = gigSellerDetails.profileImageUrl,
                TopRatedSeller = gigSellerDetails.topRated,
                ReviewsCount = gigReviews.Count(),
                AverageRating = gigReviews.Any() ? gigReviews.Average(x => x.Rating) : 0,
                Liked = favouriteGigsIdsList.Any(x => x.GigId == gig.Id)
            };

            result.Add(gigCardModel);
        }

        return result;
    }
}

