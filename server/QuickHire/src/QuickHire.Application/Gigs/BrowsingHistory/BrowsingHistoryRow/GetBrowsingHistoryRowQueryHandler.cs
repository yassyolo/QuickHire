using Mapster;
using QuickHire.Application.Admin.Models.Gigs;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Gigs.BrowsingHistory.BrowsingHistoryRow;

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

        var favouriteGigsQueryable = _repository.GetAllReadOnly<FavouriteGig>().Where(x => x.BuyerId == buyerId);
        var favouriteGigsIdsList = await _repository.ToListAsync<FavouriteGig>(favouriteGigsQueryable);
        
        var browsingHistoryQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.BrowsingHistory>().Where(bh => bh.BuyerId == buyerId).OrderByDescending(bh => bh.ViewedAt).Take(10);
        browsingHistoryQueryable = _repository.GetAllIncluding<QuickHire.Domain.Users.BrowsingHistory>(x => x.Gig);
        var browsingHistoryList = await _repository.ToListAsync<QuickHire.Domain.Users.BrowsingHistory>(browsingHistoryQueryable);

        return browsingHistoryList
            .Select(x => new BrowsingHistoryRowModel()
            {
                Id = x.Id,
                Title = x.Gig.Title,
                ImageUrl = x.Gig.ImageUrls.FirstOrDefault(),
                Liked = favouriteGigsIdsList.Any(fg => fg.GigId == x.GigId)
            }).ToList();
                
    }
}
