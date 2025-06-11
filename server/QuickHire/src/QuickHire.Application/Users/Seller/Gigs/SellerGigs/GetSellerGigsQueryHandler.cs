using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Shared;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Seller.Gigs.SellerGigs;

public class GetSellerGigsQueryHandler : IQueryHandler<GetSellerGigsQuery, IEnumerable<GigCardModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetSellerGigsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<GigCardModel>> Handle(GetSellerGigsQuery request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();

        var gigsQueryable = _repository.GetAllIncluding<Gig>(x => x.ImageUrls, x => x.PaymentPlans, x => x.Orders).Where(x => x.SellerId == sellerId);
        var gigsList = await _repository.ToListAsync<Gig>(gigsQueryable);

        var favouriteGigsQueryable = _repository.GetAllReadOnly<FavouriteGig>().Where(x => x.BuyerId == buyerId);
        var favouriteGigsIdsList = await _repository.ToListAsync<FavouriteGig>(favouriteGigsQueryable);

var result = new List<GigCardModel>();  

foreach(var gig in gigsList)
{
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

