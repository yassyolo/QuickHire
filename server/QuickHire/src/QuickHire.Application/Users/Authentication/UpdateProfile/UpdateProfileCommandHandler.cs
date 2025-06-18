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
        try
        {
            var result = await _userService.UpdateCurrentUser(request.FullName, request.Email, request.Username, request.CountryId, request.City, request.ZipCode, request.Street);

            var buyerId = await _userService.GetBuyerIdByUserIdAsync();

            await _notificationService.MakeNotification(buyerId, Common.Interfaces.Factories.Notification.NotificationRecipientType.Buyer, Domain.Users.Enums.NotificationType.ProfileUpdate, new Dictionary<string, string> { });
        }
        catch (Exception ex)
        {
            throw new BadRequestException("An error occurred while updating the profile.", ex.Message);
        }       

        return Unit.Value;
    }
}
