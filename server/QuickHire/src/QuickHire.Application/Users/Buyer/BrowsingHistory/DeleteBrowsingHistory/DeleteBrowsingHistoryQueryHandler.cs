using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Buyer.BrowsingHistory.DeleteBrowsingHistory;

public class DeleteBrowsingHistoryQueryHandler : IQueryHandler<DeleteBrowsingHistoryQuery, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public DeleteBrowsingHistoryQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Unit> Handle(DeleteBrowsingHistoryQuery request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();

        var browsingHistoryQueryable = _repository.GetAllReadOnly<Domain.Users.BrowsingHistory>().Where(bh => bh.BuyerId == buyerId).OrderByDescending(bh => bh.ViewedAt).Take(10);
        var browsingHistoryList = await _repository.ToListAsync(browsingHistoryQueryable);

        if (browsingHistoryList.Any())
        {
            foreach (var browsingHistory in browsingHistoryList)
            {
                await _repository.DeleteAsync(browsingHistory);
            }
        }

        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

