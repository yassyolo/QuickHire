using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;

namespace QuickHire.Application.Gigs.FavouriteLists.UnfavouriteGig;

public class UnfavouriteGigCommandHander : ICommandHandler<UnfavouriteGigCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public UnfavouriteGigCommandHander(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<Unit> Handle(UnfavouriteGigCommand request, CancellationToken cancellationToken)
    {
        /*var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        var favoruriteGigQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.FavouriteGig>().Where(x => x.BuyerId == buyerId && x.GigId == request.Id);
        var favouriteGigsList = await _repository.ToListAsync<QuickHire.Domain.Users.FavouriteGig>(favoruriteGigQueryable);

        if (favouriteGigsList.Any())
        {
            foreach (var favouriteGig in favouriteGigsList)
            {
                await _repository.DeleteAsync(favouriteGig);
            }
        }

        await _repository.SaveChangesAsync();*/
        return Unit.Value;
    }
}
