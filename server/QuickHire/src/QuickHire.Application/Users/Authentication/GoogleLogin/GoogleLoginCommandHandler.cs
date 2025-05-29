using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;
using System.Security.Claims;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

namespace QuickHire.Application.Users.Authentication.GoogleLogin;

public class GoogleLoginCommandHandler : ICommandHandler<GoogleLoginCommand, Unit>
{
    private readonly IUserService _userService;

    public GoogleLoginCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Unit> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var context = request.HttpContext;
        var result = await context.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Google authentication failed.");
        }

        var externalInfo = await _userService.GetExternalLoginInfoAsync();
        if (externalInfo == null)
        {
            throw new QuickHire.Domain.Shared.Exceptions.BadRequestException("External login information not found.", "");
        }

        var email = externalInfo.Principal.FindFirstValue(ClaimTypes.Email);
        var user = await _userService.FindByExternalLoginAsync(externalInfo.LoginProvider, externalInfo.ProviderKey) ?? await _userService.GetUserByEmailAsync(email);

        if(user == null)
        {
            var createdUserResult = await _userService.CreateUserForExternalLoginAsync(externalInfo);
        }

        await _userService.AssignJwtToken(user);
        await _userService.AssignRefreshToken(user);

        return Unit.Value;
    }
}

