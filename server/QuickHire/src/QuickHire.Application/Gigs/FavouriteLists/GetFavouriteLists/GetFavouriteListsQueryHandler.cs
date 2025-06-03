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
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IEnumerable<string> ImageUrls { get; set; } = new List<string>();
    public int GigCount { get; set; }
    public async Task<IEnumerable<FavouriteListModel>> Handle(GetFavouriteListsQuery request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();
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
        });
    }
}


