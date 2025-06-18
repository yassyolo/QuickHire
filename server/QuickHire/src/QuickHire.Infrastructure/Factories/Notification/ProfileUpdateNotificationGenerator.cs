using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class ProfileUpdateNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.ProfileUpdate;
    public string Title { get; set; } = "Profile Update";
    public string Message { get; set; } = "Hello! Your profile has been successfully updated.";

    public Domain.Users.Notification Generate(int recipientId, NotificationRecipientType recipientType, Dictionary<string, string>? placeholders = null)
    {
        string finalTitle = ReplacePlaceHolders(Title, placeholders);
        string finalMessage = ReplacePlaceHolders(Message, placeholders);

        var notification = new Domain.Users.Notification
        {
            CreatedAt = DateTime.Now,
            IsRead = false,
            Title = finalTitle,
            Message = finalMessage,
            Sent = false
        };

        if (recipientType == NotificationRecipientType.Buyer)
            notification.BuyerId = recipientId;
        else if (recipientType == NotificationRecipientType.Seller)
            notification.SellerId = recipientId;

        return notification;
    }
}
