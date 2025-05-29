using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class CustomRequestReceivedNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.CustomRequestReceived;

    public string Title { get; set; } = "Your Received New Custom Request!";
    public string Message { get; set; } = "Hello, {UserName}! You have received custom request with number {CustomRequestNumber}. Click here to review or respond to it: {ConversationId}.";

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
