using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Moderation;
using QuickHire.Domain.Moderation.Enums;
using QuickHire.Domain.Shared.Exceptions;


namespace QuickHire.Application.Admin.Gigs.DeactivateGigAdmin;

public class DeactivateGigAdminCommandHandler : ICommandHandler<DeactivateGigAdminCommand, Unit>
{
    private readonly IRepository _repository;

    public DeactivateGigAdminCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeactivateGigAdminCommand request, CancellationToken cancellationToken)
    {
        /*var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var reportedItem = new ReportedItem();
        if(gig.ModerationStatus == ModerationStatus.PendingReview)
        {
            var reportedItemQueryable = _repository.GetAllReadOnly<ReportedItem>().Where(x => x.GigId == request.Id);

            reportedItem = await _repository.FirstOrDefaultAsync(reportedItemQueryable);
        }

        gig.ModerationStatus = ModerationStatus.Deactivated;

        var deactivatedRecord = new DeactivatedRecord
        {
            GigId = request.Id,
            Reason = request.Reason,
            CreatedAt = DateTime.Now,
        };

        if(reportedItem != null)
        {
            deactivatedRecord.ReportedItemId = reportedItem.Id;
        }

        await _repository.AddAsync(deactivatedRecord);
        await _repository.UpdateAsync(gig);

        await _repository.SaveChangesAsync();*/
        return Unit.Value;
    }
}

