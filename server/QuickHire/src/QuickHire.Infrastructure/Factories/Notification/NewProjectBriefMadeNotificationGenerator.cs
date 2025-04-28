using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class NewProjectBriefMadeNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.NewProjectBriefMade;

    public string Title { get; set; } = "A New Project Brief Has Been Created!";
    public string Message { get; set; } = "Hello, {UserName}! A new project brief titled '{ProjectTitle}' has been created. Please review it and provide your feedback: {ProjectBriefId}.";

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
