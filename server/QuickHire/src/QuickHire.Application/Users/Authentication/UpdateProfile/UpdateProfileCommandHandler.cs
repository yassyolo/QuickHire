using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Authentication.UpdateProfile;

public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand, Unit>
{
    private readonly IUserService _userService;
    private readonly INotificationService _notificationService;

    public UpdateProfileCommandHandler(IUserService userService, INotificationService notificationService)
    {
        _userService = userService;
        _notificationService = notificationService;
    }
    public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateCurrentUser(request.FullName, request.Email, request.Username, request.CountryId, request.City, request.ZipCode, request.Street);

        if (!result.isSuccess)
        {
            throw new BadRequestException("Failed to update profile", "");
        }

        await _notificationService.MakeNotification(result.userId, Common.Interfaces.Factories.Notification.NotificationRecipientType.Seller, Domain.Users.Enums.NotificationType.ProfileUpdate,
        new Dictionary<string, string> { { "UserName", result.username! } });

        return Unit.Value;
    }
}
