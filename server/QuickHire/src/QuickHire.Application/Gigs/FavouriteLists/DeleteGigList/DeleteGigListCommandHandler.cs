using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Gigs.FavouriteLists.DeleteGigList;

public class DeleteGigListCommandHandler : ICommandHandler<DeleteGigListCommand, Unit>
{
    private readonly IRepository _repository;

    public DeleteGigListCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteGigListCommand request, CancellationToken cancellationToken)
    {
        var gigListQueryable = _repository.GetAllReadOnly<FavouriteGigsList>().Where(x => x.Id == request.Id);
        gigListQueryable = _repository.GetAllIncluding<FavouriteGigsList>(x => x.FavouriteGigs);
        var gigList = await _repository.FirstOrDefaultAsync<FavouriteGigsList>(gigListQueryable);
        if (gigList == null)
        {
            throw new NotFoundException(nameof(FavouriteGigsList), request.Id);
        }

        foreach (var gig in gigList.FavouriteGigs)
        {
            await _repository.DeleteAsync(gig);
        }

        await _repository.DeleteAsync(gigList);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

