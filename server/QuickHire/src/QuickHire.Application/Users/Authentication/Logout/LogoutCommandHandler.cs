using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Authentication.Logout;

internal class LogoutCommandHandler : ICommandHandler<LogoutCommand, Unit>
{
    private readonly IUserService _userService;

    public LogoutCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
       var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new NotFoundException("User not found", "User not found.");
        }

        await _userService.LogoutUserAsync(user);

        return Unit.Value;
    }
}

