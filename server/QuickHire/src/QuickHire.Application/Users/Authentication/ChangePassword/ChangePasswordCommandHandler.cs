using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;
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

        try
        {
            await _userService.ChangePasswordAsync(request.NewPassword);

            var buyerId = await _userService.GetBuyerIdByUserIdAsync();

            await _notificationService.MakeNotification(buyerId, Common.Interfaces.Factories.Notification.NotificationRecipientType.Buyer, Domain.Users.Enums.NotificationType.ProfileUpdate, new Dictionary<string, string> { });
        }
        catch (Exception ex)
        {
            throw new BadRequestException("An error occurred while changing the password.", ex.Message);
        }      

        return Unit.Value;
    }
}

