using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.FavouriteLists;
using QuickHire.Domain.Users;
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
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        var favouriteListsQueryable = _repository.GetAllIncluding<Domain.Users.FavouriteGigsList>().Where(x => x.BuyerId == buyerId);
        var favouriteLists = await _repository.ToListAsync<Domain.Users.FavouriteGigsList>(favouriteListsQueryable);

        var models = await Task.WhenAll(favouriteLists.Select(async x =>
        {
            var favourriteGigsQueryable = _repository.GetAllIncluding<Domain.Users.FavouriteGig>(x => x.Gig).Where(s => s.FavouriteGigsListId == x.Id);
            var favouriteGigsList = await _repository.ToListAsync<Domain.Users.FavouriteGig>(favourriteGigsQueryable);
            var images = favouriteGigsList.SelectMany(fg => fg.Gig.ImageUrls.Take(1)).Distinct()
                .Take(3)
                .ToList() ?? new List<string>();

            return new FavouriteListModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description!,
                GigCount = x.FavouriteGigs?.Count() ?? 0,
                ImageUrls = images
            };
        }));

        return models;
    }
}


