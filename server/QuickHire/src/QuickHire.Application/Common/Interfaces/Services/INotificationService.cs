using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users;
using QuickHire.Domain.Users.Enums;

namespace QuickHire.Application.Common.Interfaces.Services;

public interface INotificationService
{
    Task MarkNotificationAsRead(int id);
    Task MakeNotification(int recipientId, NotificationRecipientType recipientType, NotificationType type, Dictionary<string, string>? placeholders = null);
    Task<IEnumerable<Notification>> GetUserNotifications(bool buyer);

}
