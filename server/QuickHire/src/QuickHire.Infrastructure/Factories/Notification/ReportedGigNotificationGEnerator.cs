using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;


namespace QuickHire.Infrastructure.Factories.Notification;

public class ReportedGigNotificationGEnerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.ReportedGig;

    public string Title { get; set; } = "Your gig has been reported";

    public string Message { get; set; } =
        "One of your gigs  \"{GigTitle}\" has been reported for review. " +
        "Reason: {ReportReason}. Our moderation team will investigate and take any necessary actions. ";

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
