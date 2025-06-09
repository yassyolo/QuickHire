using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

namespace QuickHire.Application.Users.Authentication.Login;

internal class LoginBuyerCommandHandler : ICommandHandler<LoginBuyerCommand, Unit>
{
    private readonly IUserService _userService;

    public LoginBuyerCommandHandler(IUserService authenticationService)
    {
        _userService = authenticationService;
    }

    async Task<Unit> IRequestHandler<LoginBuyerCommand, Unit>.Handle(LoginBuyerCommand request, CancellationToken cancellationToken)
    {
         var user = await _userService.GetUserByUsernameOrEmailAsync(request.model.EmailOrUsername);

        if (user == null)
        {
            throw new NotFoundException("ApplicationUser", request.model.EmailOrUsername);
        }

        var passwordCheckResult = await _userService.CheckPasswordAsync(user, request.model.Password);

        if(!passwordCheckResult)
        {
            throw new UnauthorizedAccessException("Invalid credentials", "The provided email/username or password is incorrect.");
        }

        await _userService.AssignJwtTokens(user, "buyer");

        return Unit.Value;
    }
}
