using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Gigs.Seller.GetGigForDelete;

public class GetGigForDeleteQueryHandler : IQueryHandler<GetGigForDeleteQuery, string[]>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetGigForDeleteQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<string[]> Handle(GetGigForDeleteQuery request, CancellationToken cancellationToken)
    {
        var gigQueryable = _repository.GetAllIncluding<Gig>(x => x.Orders).Where(x => x.Id == request.Id);
        var gig = await _repository.FirstOrDefaultAsync(gigQueryable);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var ordersInProgress = gig.Orders.Where(x => x.Status == Domain.Orders.Enums.OrderStatus.InProgress).Select(x => x.OrderNumber).ToArray();

        if (ordersInProgress.Any())
        {
            return ordersInProgress;
        }

        return Array.Empty<string>();
    }
}

