using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

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
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        var favouriteGigsQueryable = _repository.GetAllIncluding<FavouriteGig>(x => x.Gig).Where(x => x.GigId == request.Id  && x.BuyerId == buyerId);
        var favouriteGig = await _repository.FirstOrDefaultAsync<FavouriteGig>(favouriteGigsQueryable);

        if(favouriteGig == null)
        {
            throw new NotFoundException(nameof(FavouriteGig), request.Id);
        }

        if(favouriteGig.BuyerId != buyerId)
        {
            throw new UnauthorizedAccessException("You are not authorized to unfavourite this gig.");
        }

        await _repository.DeleteAsync(favouriteGig);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}
