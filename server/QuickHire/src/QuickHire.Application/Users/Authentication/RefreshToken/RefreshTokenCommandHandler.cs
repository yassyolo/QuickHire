using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

namespace QuickHire.Application.Users.Authentication.RefreshToken;

internal class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Unit>
{
    private readonly IUserService _userService;

    public RefreshTokenCommandHandler(IUserService authenticationService)
    {
        _userService = authenticationService;
    }

    public async Task<Unit> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userService.GetUserByRefreshTokenAsync(request.Token);

            await _userService.AssignJwtTokens(user, user.Mode);
        }
        catch (Exception)
        {
            throw;
        }       

        return Unit.Value;
    }
}

