using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

namespace QuickHire.Application.Gigs.FavouriteLists.RemoveGigFromList;

public class RemoveGigFromListCommandHandler : ICommandHandler<RemoveGigFromListCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public RemoveGigFromListCommandHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Unit> Handle(RemoveGigFromListCommand request, CancellationToken cancellationToken)
    {
        var favouriteGig = await _repository.GetByIdAsync<FavouriteGig, int>(request.FavouriteGigId);
        if (favouriteGig == null)
        {
            throw new NotFoundException(nameof(FavouriteGig), request.FavouriteGigId);
        }

        await _repository.DeleteAsync(favouriteGig);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

