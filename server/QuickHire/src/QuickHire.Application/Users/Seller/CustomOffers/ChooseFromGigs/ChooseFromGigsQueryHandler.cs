using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.CustomOffers;
using QuickHire.Domain.Gigs;

namespace QuickHire.Application.Users.Seller.CustomOffers.ChooseFromGigs;

public class ChooseFromGigsQueryHandler : IQueryHandler<ChooseFromGigsQuery, List<ChooseFromGigsModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public ChooseFromGigsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<List<ChooseFromGigsModel>> Handle(ChooseFromGigsQuery request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetBuyerIdByUserIdAsync();
        var gigsQueryable = _repository.GetAllIncluding<Gig>().Where(x => x.SellerId == sellerId);
        var gigsList = await _repository.ToListAsync<Gig>(gigsQueryable);
        return gigsList.Select(x => new ChooseFromGigsModel
        {
            Id = x.Id,
            Title = x.Title,
            ImageUrl = x.ImageUrls.FirstOrDefault() ?? string.Empty
        }).ToList();           
    }
}
