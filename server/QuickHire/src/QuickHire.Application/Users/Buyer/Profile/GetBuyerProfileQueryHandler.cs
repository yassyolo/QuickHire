using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Buyer.Profile;
public class GetBuyerProfileQueryHandler : IQueryHandler<GetBuyerProfileQuery, BuyerProfileModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetBuyerProfileQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<BuyerProfileModel> Handle(GetBuyerProfileQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetBuyerProfileAsync();
    }
}

