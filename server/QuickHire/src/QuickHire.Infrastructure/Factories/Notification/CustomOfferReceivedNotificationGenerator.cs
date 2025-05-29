using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class CustomOfferReceivedNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.CustomOfferReceived;

    public string Title { get; set; } = "You Have Received a New Custom Offer!";
    public string Message { get; set; } = "Hello, {UserName}! A new custom offer with number {CustomOfferNumber} has been received. Click here to review and respond: {ConversationId}.";

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
