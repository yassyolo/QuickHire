using Mapster;
using QuickHire.Application.Admin.Models.Users.Notifications;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;

namespace QuickHire.Application.Users.Notifications.GetNotifications;

public class GetNotificationsQueryHandler : IQueryHandler<GetNotificationsQuery, IEnumerable<GetNotificationsResponseModel>>
{
    private readonly INotificationService _notificationService;

    public GetNotificationsQueryHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }


    public async Task<IEnumerable<GetNotificationsResponseModel>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        /*var notifications = await _notificationService.GetUserNotifications(request.Buyer);

        return notifications.Adapt<IEnumerable<GetNotificationsResponseModel>>();*/

        return new List<GetNotificationsResponseModel>
    {
            new GetNotificationsResponseModel
            {
                Id = 1,
                Title = "Sample Notification",
                Message = "This is a sample notification message.",
                CreatedAt = DateTime.UtcNow.ToString("o")
            },
            new GetNotificationsResponseModel
            {
                Id = 2,
                Title = "Another Notification",
                Message = "This is another sample notification message.",
                CreatedAt = DateTime.UtcNow.ToString("o")
            }

    };
    }

}

