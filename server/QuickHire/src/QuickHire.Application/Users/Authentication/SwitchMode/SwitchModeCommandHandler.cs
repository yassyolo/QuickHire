using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;

namespace QuickHire.Application.Users.Authentication.SwitchMode;

public class SwitchModeCommandHandler : ICommandHandler<SwitchModeCommand, Unit>
{
    private readonly IUserService _userService;

    public SwitchModeCommandHandler(IUserService authenticationService)
    {
        _userService = authenticationService;
    }

    public async Task<Unit> Handle(SwitchModeCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetCurrentUserAsync();
        await _userService.AssignJwtTokens(user, request.Mode);

        return Unit.Value;
    }
}

