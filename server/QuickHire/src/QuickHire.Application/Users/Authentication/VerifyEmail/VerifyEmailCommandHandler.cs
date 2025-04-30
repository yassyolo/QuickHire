using MediatR;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Authentication.VerifyEmail;

internal class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, VerifyEmailResponseModel>
{
    private readonly IUserService _userService;

    public VerifyEmailCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<VerifyEmailResponseModel> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByUserIdAsync(request.model.UserId);

        if(user == null)
        {
            throw new NotFoundException("User not found", $"User with id: {request.model.UserId} not found.");
        }

        if (user.EmailConfirmed)
        {
            throw new BadRequestException("Email already verified", $"Email {user.Email} is already verified.");
        }

        var result = await _userService.VerifyEmailAsync(user.Id, request.model.Token);

        if (!result.IsSuccess)
        {
            throw new BadRequestException("Invalid token", string.Join(";", result.Errors.Select(x => x.ToString())));
        }

        return new VerifyEmailResponseModel
        {
            Message = "Email verified successfully"
        };
    }
}
