using MediatR;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Authentication.Logout;

internal class GoogleLogoutCommandHandler : ICommandHandler<GoogleLogoutCommand, Unit>
{
    public GoogleLogoutCommandHandler()
    {
    }

    public async Task<Unit> Handle(GoogleLogoutCommand request, CancellationToken cancellationToken)
    {
        var context = request.HttpContext;
        await context.SignOutAsync(IdentityConstants.ExternalScheme);

        return Unit.Value;
    }
}

