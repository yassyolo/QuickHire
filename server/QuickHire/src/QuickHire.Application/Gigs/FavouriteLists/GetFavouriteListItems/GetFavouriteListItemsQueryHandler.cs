using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.FavouriteLists;
using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Gigs.FavouriteLists.GetFavouriteListItems;

public class GetFavouriteListItemsQueryHandler : IQueryHandler<GetFavouriteListItemsQuery, FavouriteListItemModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetFavouriteListItemsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<FavouriteListItemModel> Handle(GetFavouriteListItemsQuery request, CancellationToken cancellationToken)
    {
       /* var favouriteListQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.FavouriteGigsList>()
            .Where(x => x.Id == request.Id);
        var favouriteList = await _repository.FirstOrDefaultAsync<QuickHire.Domain.Users.FavouriteGigsList>(favouriteListQueryable);

        var result = new FavouriteListItemModel()
        {
            Id = favouriteList.Id,
            Name = favouriteList.Name,
            Description = favouriteList.Description,
        };

        var gigCardresult = new List<GigCardModel>();

        foreach (var bh in favouriteList.FavouriteGigs)
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
                Liked = favouriteList.FavouriteGigs.Any(x => x.GigId == gig.Id)
            };

            gigCardresult.Add(gigCardModel);
        }

        result.Gigs = gigCardresult;
        return result;*/

        return new FavouriteListItemModel
        {
            Id = request.Id,
            Name = "Sample Favourite List",
            Description = "This is a sample description for the favourite list.",
            Gigs = new List<GigCardModel>
            {
                new GigCardModel
                {
                    Id = 1,
                    Title = "Sample Gig",
                    FromPrice = 100.00m,
                    ImageUrls = new List<string> { "https://picsum.photos/200/300" },
                    SellerName = "Sample Seller",
                    SellerId = 1,
                    SellerProfileImageUrl = "https://picsum.photos/200/300",
                    TopRatedSeller = true,
                    ReviewsCount = 10,
                    AverageRating = 4.5,
                    Liked = true
                }
            }
        };
    }
}

