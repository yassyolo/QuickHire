using QuickHire.Application.Admin.Models.Users;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;

namespace QuickHire.Application.Admin.Gigs.SellerForGig;

public class GetSellerForGigQueryHandler : IQueryHandler<GetSellerForGigQuery, UserForAdminModel>
{
    private readonly IUserService _userService;

    public GetSellerForGigQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserForAdminModel> Handle(GetSellerForGigQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetSellerForGigAsync(request.Id);
    }
}

