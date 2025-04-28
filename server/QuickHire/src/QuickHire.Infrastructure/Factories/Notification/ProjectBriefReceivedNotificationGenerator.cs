using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class ProjectBriefReceivedNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.ProjectBriefReceived;

    public string Title { get; set; } = "You Received A New Project Brief!";
    public string Message { get; set; } = "Hello, {UserName}! A new project brief with number {ProjectBriefNumber} has been received. Please review the details and start preparing your offer: {PtojectBriefId}.";

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

