using iText.Commons.Bouncycastle.Cms;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class CustomOfferAcceptedNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.CustomOfferAccepted;

    public string Title { get; set; } = "Your Custom Offer Has Been Accepted!";
    public string Message { get; set; } = "Hello, {UserName}! Congratulations! Your custom offer with the number {CustomOfferNumber} has been accepted and ordered. Click here to view the order details: {OrderId}";

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
