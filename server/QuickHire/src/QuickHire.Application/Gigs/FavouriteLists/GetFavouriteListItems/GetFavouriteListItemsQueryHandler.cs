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
        var favouriteListQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.FavouriteGigsList>()
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
        return result;
    }
}

