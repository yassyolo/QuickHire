using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Gigs.FavouriteLists.EditFavouriteList;

public class EditFavouriteListCommandHandler : ICommandHandler<EditFavouriteListCommand, Unit>
{
    private readonly IRepository _repository;

    public EditFavouriteListCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(EditFavouriteListCommand request, CancellationToken cancellationToken)
    {
        var gigList = await _repository.GetByIdAsync<FavouriteGigsList, int>(request.Id);
        if (gigList == null)
        {
            throw new NotFoundException(nameof(FavouriteGigsList), request.Id);
        }

        gigList.Name = request.Name;
        gigList.Description = request.Description;

        await _repository.UpdateAsync(gigList);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

