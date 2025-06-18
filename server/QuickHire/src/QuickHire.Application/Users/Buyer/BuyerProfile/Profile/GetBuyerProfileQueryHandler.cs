using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Buyer.BuyerProfile.Profile;
public class GetBuyerProfileQueryHandler : IQueryHandler<GetBuyerProfileQuery, BuyerProfileModel>
{
    private readonly IUserService _userService;

    public GetBuyerProfileQueryHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<BuyerProfileModel> Handle(GetBuyerProfileQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetBuyerProfileAsync();
    }
}

