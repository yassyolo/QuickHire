using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

namespace QuickHire.Application.Gigs.FavouriteLists.SaveGigToOldList;

public class SaveGigToOldListCommandHandler : ICommandHandler<SaveGigToOldListCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public SaveGigToOldListCommandHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Unit> Handle(SaveGigToOldListCommand request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        var favouriteGigList = await _repository.GetByIdAsync<QuickHire.Domain.Users.FavouriteGigsList, int>(request.FavouriteListId);
        if (favouriteGigList == null)
        {
            throw new NotFoundException(nameof(QuickHire.Domain.Users.FavouriteGigsList), request.FavouriteListId);
        }

        if (favouriteGigList.BuyerId != buyerId)
        {
            throw new UnauthorizedAccessException("You do not have permission to access this favourite list.");
        }

        var gig = await _repository.GetByIdAsync<QuickHire.Domain.Gigs.Gig, int>(request.GigId);
        if (gig == null)
        {
            throw new NotFoundException(nameof(QuickHire.Domain.Gigs.Gig), request.GigId);
        }

        var newFavouriteGig = new QuickHire.Domain.Users.FavouriteGig
        {
            BuyerId = buyerId,
            GigId = gig.Id,
            FavouriteGigsListId = favouriteGigList.Id,
            AddedAt = DateTime.Now
        };

        await _repository.AddAsync(newFavouriteGig);
        await _repository.SaveChangesAsync();

        return Unit.Value;

    }
}
