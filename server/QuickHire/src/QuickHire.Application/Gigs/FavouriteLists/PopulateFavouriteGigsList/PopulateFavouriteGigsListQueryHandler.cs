using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.FavouriteLists;

namespace QuickHire.Application.Gigs.FavouriteLists.PopulateFavouriteGigsList;

public class PopulateFavouriteGigsListQueryHandler : IQueryHandler<PopulateFavouriteGigsListQuery, IEnumerable<PopulateFavouriteGigListModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public PopulateFavouriteGigsListQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<PopulateFavouriteGigListModel>> Handle(PopulateFavouriteGigsListQuery request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        var favouriteGigsListQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.FavouriteGigsList>().Where(x => x.BuyerId == buyerId);
        var favouriteGigsList = await _repository.ToListAsync<QuickHire.Domain.Users.FavouriteGigsList>(favouriteGigsListQueryable);

        return favouriteGigsList.Select(x => new PopulateFavouriteGigListModel
        {
            Id = x.Id,
            Name = x.Name,
        });
    }
}
