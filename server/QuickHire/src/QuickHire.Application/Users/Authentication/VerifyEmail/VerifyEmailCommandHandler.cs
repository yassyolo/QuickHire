using MediatR;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Authentication.VerifyEmail;

internal class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Unit>
{
    private readonly IUserService _userService;
    private readonly INotificationService _notificationService;

    public VerifyEmailCommandHandler(IUserService userService, INotificationService notificationService)
    {
        _userService = userService;
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByUserIdAsync(request.UserId);

        if(user == null)
        {
            throw new NotFoundException("User not found", $"User with id: {request.UserId} not found.");
        }

        if (user.EmailConfirmed)
        {
            throw new BadRequestException("Email already verified", $"Email {user.Email} is already verified.");
        }

        try
        {
            var result = await _userService.VerifyEmailAsync(user.Id, request.Token);

            if (!result.IsSuccess)
            {
                throw new BadRequestException("Invalid token", string.Join(";", result.Errors.Select(x => x.ToString())));
            }

            var buyerId = await _userService.GetBuyerIdByExistingUserId(request.UserId);

            await _notificationService.MakeNotification(buyerId, Common.Interfaces.Factories.Notification.NotificationRecipientType.Buyer, Domain.Users.Enums.NotificationType.ProfileMade, new Dictionary<string, string> { { "UserName", user.UserName! } });
        }
        catch (Exception ex)
        {
            throw new BadRequestException("Email verification failed", $"Failed to verify email for user {user.UserName}. Error: {ex.Message}");
        }

        return Unit.Value;
    }
}
