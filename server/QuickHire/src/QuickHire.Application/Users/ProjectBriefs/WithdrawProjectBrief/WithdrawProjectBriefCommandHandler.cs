using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.ProjectBriefs.Enums;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.ProjectBriefs.WithdrawProjectBrief;

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
        var projectBriefQueryable = _repository.GetAll<Domain.ProjectBriefs.ProjectBrief>().Where(x => x.Id == request.Id);
        projectBriefQueryable = _repository.GetAllIncluding<Domain.ProjectBriefs.ProjectBrief>(x => x.CustomOffers);
        var projectBrief = await _repository.FirstOrDefaultAsync<Domain.ProjectBriefs.ProjectBrief>(projectBriefQueryable); 
        if (projectBrief == null)
        {
            throw new NotFoundException(nameof(Domain.ProjectBriefs.ProjectBrief), request.Id);
        }

        bool isAssociatedWithOrder = projectBrief.Status == ProjectBriefStatus.OrderPlaced ||
       projectBrief.CustomOffers.Any(x =>
           x.Status == Domain.CustomOffers.Enums.CustomOfferStatus.Accepted &&
           x.Order != null &&
           x.Order.Status == Domain.Orders.Enums.OrderStatus.InProgress);

        if (isAssociatedWithOrder)
        {
            throw new BadRequestException("Project brief is currently associated with an active order and cannot be withdrawn.", "");
        }

        projectBrief.IsDeleted = true;
        projectBrief.DeletedAt = DateTime.Now;

        await _repository.UpdateAsync(projectBrief);
        await _repository.SaveChangesAsync();
        return Unit.Value;
    }
}

