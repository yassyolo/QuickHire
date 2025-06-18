using iText.Commons.Bouncycastle.Cms;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class ProfileMadeNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.ProfileMade;
    public string Title { get; set; } = "Welcome to QuickHire";
    public string Message { get; set; } = "Welcome, {UserName}! Your profile has been successfully created. Take a moment to complete your details and start using all the features available to you.";

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
