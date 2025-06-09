using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.FavouriteLists;
using System.Collections.Generic;

namespace QuickHire.Application.Gigs.FavouriteLists.GetFavouriteLists;

public class GetFavouriteListsQueryHandler : IQueryHandler<GetFavouriteListsQuery, IEnumerable<FavouriteListModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetFavouriteListsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<IEnumerable<FavouriteListModel>> Handle(GetFavouriteListsQuery request, CancellationToken cancellationToken)
    {
        /*var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        var favouriteListsQueryable = _repository.GetAllReadOnly<Domain.Users.FavouriteGigsList>().Where(x => x.BuyerId == buyerId);
        favouriteListsQueryable = _repository.GetAllIncluding<Domain.Users.FavouriteGigsList>(x => x.FavouriteGigs);
        var favouriteLists = await _repository.ToListAsync<Domain.Users.FavouriteGigsList>(favouriteListsQueryable);

        return favouriteLists.Select(x => new FavouriteListModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            GigCount = x.FavouriteGigs.Count(),
            ImageUrls = x.FavouriteGigs.SelectMany(fg => fg.Gig.ImageUrls.Take(1)).Distinct().Take(3)
        });*/

        return new List<FavouriteListModel>
        {
            new FavouriteListModel
            {
                Id = 1,
                Name = "My Favourite Gigs",
                Description = "A collection of my favourite gigs.",
                GigCount = 5,
                ImageUrls = new List<string> { "https://picsum.photos/200/300", "https://picsum.photos/200/300" }
            },
            new FavouriteListModel
            {
                Id = 2,
                Name = "Top Rated Gigs",
                Description = "Gigs that have received the highest ratings.",
                GigCount = 3,
                ImageUrls = new List<string> { "https://picsum.photos/200/300" }
            }
        };
    }
}


