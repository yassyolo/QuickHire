using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Shared;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Buyer.FirstPage.RecentlyLiked;

public class RecentlyLikedQueryHandler : IQueryHandler<RecentlyLikedQuery, List<GigCardModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public RecentlyLikedQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<List<GigCardModel>> Handle(RecentlyLikedQuery request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        var favouriteGigsQueryable = _repository.GetAllReadOnly<FavouriteGig>().Where(x => x.BuyerId == buyerId);
        var favouriteGigsIdsList = await _repository.ToListAsync(favouriteGigsQueryable);
        var favouriteGigsIds = favouriteGigsIdsList.Select(x => x.GigId).ToList();

        var gigsQueryable = _repository.GetAllIncluding<Domain.Gigs.Gig>(x => x.Seller, x => x.PaymentPlans, x => x.Orders).Where(x => favouriteGigsIds.Contains(x.Id));
        var gigsList = await _repository.ToListAsync(gigsQueryable);
        gigsList = gigsList.OrderByDescending(x => x.Clicks).Take(3).ToList();
        var result = new List<GigCardModel>();

        foreach (var bh in gigsList)
        {
            var gig = bh;
            var gigSellerDetails = await _userService.GetSellerDetailsForGigCardByIdAsync(gig.SellerId);
            var gigReviewsQueryable = _repository.GetAllIncluding<Review>(x => x.Order).Where(x => gig.Orders.Select(x => x.Id).ToList().Contains(x.OrderId));
            var gigReviews = await _repository.ToListAsync(gigReviewsQueryable);

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
                Liked = true
            };

            result.Add(gigCardModel);
        }

        return result;
    }
}
