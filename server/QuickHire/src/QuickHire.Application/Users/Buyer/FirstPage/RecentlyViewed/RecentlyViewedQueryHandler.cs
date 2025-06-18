using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Shared;
using QuickHire.Domain.Users;
using System.Linq;

namespace QuickHire.Application.Users.Buyer.FirstPage.RecentlyViewed;

public class RecentlyViewedQueryHandler : IQueryHandler<RecentlyViewedQuery, List<GigCardModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public RecentlyViewedQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<List<GigCardModel>> Handle(RecentlyViewedQuery request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        var favouriteGigsQueryable = _repository.GetAllReadOnly<FavouriteGig>().Where(x => x.BuyerId == buyerId);
        var favouriteGigsIdsList = await _repository.ToListAsync(favouriteGigsQueryable);

        var browsingHistoryQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.BrowsingHistory>().Where(x => x.BuyerId == buyerId);
        var browsingHistoryList = await _repository.ToListAsync(browsingHistoryQueryable);
        var browsingHistoryListIds = browsingHistoryList.OrderByDescending(x => x.ViewedAt).Select(x => x.GigId);

        var gigsQueryable = _repository.GetAllIncluding<Domain.Gigs.Gig>(x => x.Seller).Where(x => x.ModerationStatus != Domain.Moderation.Enums.ModerationStatus.PendingReview && browsingHistoryListIds.Contains(x.Id));
        var gigsList = await _repository.ToListAsync(gigsQueryable);
        gigsList = gigsList.OrderByDescending(x => x.Clicks).Take(4).ToList();
        var result = new List<GigCardModel>();

        foreach (var bh in gigsList)
        {
            var gig = bh;
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

