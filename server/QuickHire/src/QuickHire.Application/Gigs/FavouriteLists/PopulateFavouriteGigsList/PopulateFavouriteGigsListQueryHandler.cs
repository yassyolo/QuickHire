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
        /*var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        var favouriteGigsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.FavouriteGig>().Where(x => x.BuyerId == buyerId);
        var favouriteGigsList = await _repository.ToListAsync<QuickHire.Domain.Users.FavouriteGig>(favouriteGigsQueryable);

        return favouriteGigsList.Adapt<IEnumerable<PopulateFavouriteGigListModel>>().ToList();*/

        return new List<PopulateFavouriteGigListModel>
        {
            new PopulateFavouriteGigListModel
            {
                Id = 1,
                Name = "Sample Gig 1",
            },
            new PopulateFavouriteGigListModel
            {
                Id = 2,
                Name = "Sample Gig 2",
            }
        };
    }
}
