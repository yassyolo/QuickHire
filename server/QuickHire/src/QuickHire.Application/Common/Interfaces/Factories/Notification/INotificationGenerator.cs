namespace QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users;
using QuickHire.Domain.Users.Enums;

public interface INotificationGenerator
{
    public NotificationType Type { get; }
    public string Title { get; set; }
    public string Message { get; set; }
    Notification Generate(int recipientId, NotificationRecipientType recipientType, Dictionary<string, string>? placeholders = null);
}

public enum NotificationRecipientType
{
    Buyer,
    Seller
}
