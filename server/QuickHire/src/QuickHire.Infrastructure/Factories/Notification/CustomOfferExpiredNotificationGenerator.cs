using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class CustomOfferExpiredNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.CustomOfferExpired;

    public string Title { get; set; } = "Your Custom Offer Has Expired";
    public string Message { get; set; } = "Hello, {UserName}. We're sorry to inform you that your custom offer with number {CustomOfferNumber} has expired. Go to inbox: {ConversationId}";

    public Domain.Users.Notification Generate(string userId, Dictionary<string, string>? placeholders = null)
    {
        string finalTitle = ReplacePlaceHolders(Title, placeholders);
        string finalMessage = ReplacePlaceHolders(Message, placeholders);

        return new Domain.Users.Notification
        {
            UserId = userId,
            CreatedAt = DateTime.Now,
            IsRead = false,
            Title = finalTitle,
            Message = finalMessage,
            Sent = false

        };
    }
}
