using Mapster;
using QuickHire.Application.Admin.Models.Gigs;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Buyer.BrowsingHistory.BrowsingHistoryRow;

public class GetBrowsingHistoryRowQueryHandler : IQueryHandler<GetBrowsingHistoryRowQuery, IEnumerable<BrowsingHistoryRowModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetBrowsingHistoryRowQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<BrowsingHistoryRowModel>> Handle(GetBrowsingHistoryRowQuery request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();

        var browsingHistoryQueryable = _repository.GetAllIncluding<Domain.Users.BrowsingHistory>(x => x.Gig!).Where(bh => bh.BuyerId == buyerId).OrderByDescending(bh => bh.ViewedAt).Take(10);
        var browsingHistoryList = await _repository.ToListAsync(browsingHistoryQueryable);

        return browsingHistoryList
            .Select(x => new BrowsingHistoryRowModel()
            {
                Id = x.Id,
                Title = x.Gig!.Title,
                GigId = x.GigId!.Value,
                ImageUrl = x.Gig.ImageUrls.FirstOrDefault()!,
            }).ToList();
    }
}
