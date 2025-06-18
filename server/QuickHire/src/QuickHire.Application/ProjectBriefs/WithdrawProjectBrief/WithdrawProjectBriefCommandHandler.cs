using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.ProjectBriefs.Enums;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.ProjectBriefs.WithdrawProjectBrief;

public class WithdrawProjectBriefCommandHandler : ICommandHandler<WithdrawProjectBriefCommand, Unit>
{
    private readonly IUserService _userService;
    private readonly IRepository _repository;

    public WithdrawProjectBriefCommandHandler(IUserService userService, IRepository repository)
    {
        _userService = userService;
        _repository = repository;
    }

    public async Task<Unit> Handle(WithdrawProjectBriefCommand request, CancellationToken cancellationToken)
    {
        var projectBriefQueryable = _repository.GetAllIncluding<Domain.ProjectBriefs.ProjectBrief>(x => x.CustomOffers!)
                                               .Where(x => x.Id == request.Id);
        var projectBrief = await _repository.FirstOrDefaultAsync(projectBriefQueryable);
        if (projectBrief == null)
        {
            throw new NotFoundException(nameof(Domain.ProjectBriefs.ProjectBrief), request.Id);
        }

        bool isAssociatedWithOrder = projectBrief.Status == ProjectBriefStatus.OrderPlaced ||
            projectBrief.CustomOffers!.Any(x => x.Status == Domain.CustomOffers.Enums.CustomOfferStatus.Accepted &&  x.Order != null &&
                                               x.Order.Status == Domain.Orders.Enums.OrderStatus.InProgress);

        if (isAssociatedWithOrder)
        {
            throw new BadRequestException("Project brief is currently associated with an active order and cannot be withdrawn.", "");
        }

        try
        {
            var suitableSellerProjectBriefQueryable = _repository.GetAllIncluding<Domain.ProjectBriefs.SuitableSellerProjectBrief>(x => x.ProjectBrief).Where(x => x.ProjectBrief.Id == request.Id);
            var suitableSellerProjectBrief = await _repository.ToListAsync(suitableSellerProjectBriefQueryable);

            foreach (var suitableSeller in suitableSellerProjectBrief)
            {
                suitableSeller.ProjectBrief.IsDeleted = true;
                suitableSeller.ProjectBrief.DeletedAt = DateTime.Now;
                await _repository.UpdateAsync(suitableSeller.ProjectBrief);
            }

            projectBrief.IsDeleted = true;
            projectBrief.DeletedAt = DateTime.Now;
            await _repository.UpdateAsync(projectBrief);

            await _repository.SaveChangesAsync();  
        }
        catch (Exception ex)
        {
            throw new BadRequestException("Error while withdrawing project brief", ex.Message);
        }
        return Unit.Value;
    }

}

