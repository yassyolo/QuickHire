using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;

namespace QuickHire.Application.Users.Seller.Gigs.ToggleActivationStatus;

public class ToggleActivationStatusCommandHandler : ICommandHandler<ToggleActivationStatusCommand, Unit>
{
    private readonly IRepository _repository;

    public ToggleActivationStatusCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(ToggleActivationStatusCommand request, CancellationToken cancellationToken)
    {
        var gig = await _repository.GetByIdAsync<Domain.Gigs.Gig, int>(request.Id);
        if (gig == null)
        {
            throw new Domain.Shared.Exceptions.NotFoundException(nameof(Domain.Gigs.Gig), request.Id);
        }

        if(request.Paused == false)
        {
            gig.ModerationStatus = Domain.Moderation.Enums.ModerationStatus.Paused;
        }
        else
        {
            gig.ModerationStatus = Domain.Moderation.Enums.ModerationStatus.Active;
        }

        await _repository.UpdateAsync(gig);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

