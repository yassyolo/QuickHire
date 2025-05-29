using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Authentication.AboutUser;

internal class AboutUserQueryHandler : IQueryHandler<AboutUserQuery, AboutUserModel>
{
    private readonly IUserService _userService;

    public AboutUserQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<AboutUserModel> Handle(AboutUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new NotFoundException("User not found", "User not found.");
        }

        return user.Adapt<AboutUserModel>();
    }
}

