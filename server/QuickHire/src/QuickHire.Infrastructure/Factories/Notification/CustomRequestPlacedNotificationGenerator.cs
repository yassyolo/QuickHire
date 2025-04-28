using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class CustomRequestPlacedNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.CustomRequestPlaced;

    public string Title { get; set; } = "Your Custom Request Has Been Placed!";
    public string Message { get; set; } = "Hello, {UserName}! Your custom request with number {CustomRequestNumber} has been successfully placed. Click here to view its progress: {ConversationId}.";

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
        };
    }
}
