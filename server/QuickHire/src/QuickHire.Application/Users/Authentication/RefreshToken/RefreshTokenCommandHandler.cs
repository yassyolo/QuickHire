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
        var user = await _userService.GetUserByRefreshTokenAsync(request.Token);
        
        if (user == null)
        {
            throw new NotFoundException("User not found", $"User with refresh token: {request.Token} not found.");
        }

        if(user.RefreshTokenExpirationDate < DateTime.Now)
        {
            throw new UnauthorizedAccessException("Invalid token", "The provided refresh token is invalid.");
        }

        await _userService.AssignJwtTokens(user, user.Mode);

        return Unit.Value;
    }
}

