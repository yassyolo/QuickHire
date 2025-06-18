using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Gigs;

namespace QuickHire.Application.Gigs.Seller.SellerGigsTable;

public class GetSellerGigsTableQueryHandler : IQueryHandler<GetSellerGigsTableQuery, List<SellerGigTableRowModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetSellerGigsTableQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<List<SellerGigTableRowModel>> Handle(GetSellerGigsTableQuery request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var gisQueryable = _repository.GetAllIncluding<Domain.Gigs.Gig>(x => x.Orders).Where(x => x.SellerId == sellerId && (int)x.ModerationStatus == request.ModerationStatusId);

        var gisList = await _repository.ToListAsync(gisQueryable);
        var gigsListIds = gisList.Select(x => x.Id).ToList();
        var favouriteGigsQueryable = _repository.GetAllReadOnly<Domain.Users.FavouriteGig>().Where(x => gigsListIds.Contains(x.GigId));
        var favouriteGigs = await _repository.ToListAsync(favouriteGigsQueryable);

        var gigsTableRows = gisList.Select(gig => new SellerGigTableRowModel
        {
            Id = gig.Id,
            Clicks = gig.Clicks,
            Title = gig.Title,
            Likes = favouriteGigs.Count(),
            Orders = gig.Orders.Count(),
            Revenue = gig.Orders.Sum(x => x.TotalPrice)
        }).ToList();

        return gigsTableRows;
    }
}
