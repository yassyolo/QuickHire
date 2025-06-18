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
        return await _userService.GetAboutCurrentUserAsync();
    }
}

