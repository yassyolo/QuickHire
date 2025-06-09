using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Authentication.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, Unit>
{
    private readonly IUserService _userService;
    private readonly INotificationService _notificationService;

    public ChangePasswordCommandHandler(IUserService userService, INotificationService notificationService)
    {
        _userService = userService;
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if(request.NewPassword != request.ConfirmPassword)
        {
            throw new ArgumentException("New password and confirm new password do not match.");
        }

        await _userService.ChangePasswordAsync(request.NewPassword);

        var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var user = await _userService.GetCurrentUserAsync();

        await _notificationService.MakeNotification(sellerId, Common.Interfaces.Factories.Notification.NotificationRecipientType.Seller, Domain.Users.Enums.NotificationType.ProfileUpdate,
         new Dictionary<string, string> { { "UserName", user.UserName! } });

        return Unit.Value;
    }
}

