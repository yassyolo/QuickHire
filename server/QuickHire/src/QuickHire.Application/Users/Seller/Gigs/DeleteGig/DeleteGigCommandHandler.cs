using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Seller.Gigs.DeleteGig;

public class DeleteGigCommandHandler : ICommandHandler<DeleteGigCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public DeleteGigCommandHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Unit> Handle(DeleteGigCommand request, CancellationToken cancellationToken)
    {
        var gigQueryable = _repository.GetAllReadOnly<Gig>().Where(x => x.Id == request.Id);
        gigQueryable = _repository.GetAllIncluding<Gig>(x => x.Orders,x => x.Requirements, x => x.FAQs, x=> x.Metadata, x => x.Tags, x => x.PaymentPlans);
        var gig = await _repository.FirstOrDefaultAsync(gigQueryable);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var ordersInProgress = gig.Orders.Where(x => x.Status == Domain.Orders.Enums.OrderStatus.InProgress);

        if (ordersInProgress.Any())
        {
            throw new BadRequestException("You cannot delete a gig that has orders in progress.", "");
        }

        foreach (var requirement in gig.Requirements)
        {
            requirement.IsDeleted = true;
            requirement.DeletedAt = DateTime.Now;
            await _repository.UpdateAsync(requirement);
        }

        foreach (var faq in gig.FAQs)
        {
            faq.IsDeleted = true;
            faq.DeletedAt = DateTime.Now;
            await _repository.UpdateAsync(faq);
        }

        foreach (var metadata in gig.Metadata)
        {
            metadata.IsDeleted = true;
            metadata.DeletedAt = DateTime.Now;
            await _repository.UpdateAsync(metadata);
        }

        foreach (var tag in gig.Tags)
        {
            tag.IsDeleted = true;
            tag.DeletedAt = DateTime.Now;
            await _repository.UpdateAsync(tag);
        }

        foreach (var paymentPlan in gig.PaymentPlans)
        {
            paymentPlan.IsDeleted = true;
            paymentPlan.DeletedAt = DateTime.Now;
            await _repository.UpdateAsync(paymentPlan);
        }

        gig.IsDeleted = true;
        gig.DeletedAt = DateTime.Now;

        await _repository.UpdateAsync(gig);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

