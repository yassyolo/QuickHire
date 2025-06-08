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
        /*var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();

        var gigsQueryable = _repository.GetAllReadOnly<Gig>().Where(g => g.SellerId == sellerId);
        gigsQueryable = _repository.GetAllIncluding<Gig>(g => g.ImageUrls, g => g.PaymentPlans, g => g.Orders);
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

return result;*/

        return new List<GigCardModel>
        {
            new GigCardModel
            {
                Id = 1,
                Title = "Sample Gig",
                FromPrice = 100,
                ImageUrls = new List<string> { "https://picsum.photos/200/300" },
                SellerName = "John Doe",
                SellerId = 1,
                SellerProfileImageUrl = "https://picsum.photos/50/50",
                TopRatedSeller = true,
                ReviewsCount = 10,
                AverageRating = 4.5f,
                Liked = true
            },
            new GigCardModel
            {
                Id = 2,
                Title = "Another Gig",
                FromPrice = 200,
                ImageUrls = new List<string> { "https://picsum.photos/200/300" },
                SellerName = "Jane Smith",
                SellerId = 2,
                SellerProfileImageUrl = "https://picsum.photos/50/50",
                TopRatedSeller = false,
                ReviewsCount = 5,
                AverageRating = 3.8f,
                Liked = false
            }
        };
    }
}

