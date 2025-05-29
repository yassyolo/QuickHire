using QuickHire.Domain.Users;
using QuickHire.Domain.Users.Enums;

namespace QuickHire.Application.Common.Interfaces.Services;

public interface INotificationService
{
    public Task MarkNotificationAsRead(int id);
    public Task MakeNotification(string userId, NotificationType type, Dictionary<string, string>? placeholders = null);
    public Task<IEnumerable<Notification>> GetUserNotifications(string userId);

}
