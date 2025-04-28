using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class ProfileUpdateNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.ProfileUpdate;
    public string Title { get; set; } = "Your Profile Has been updated";
    public string Message { get; set; } = "Hello, {UserName}! Your profile has been successfully updated. Please review the changes and let us know if anything looks incorrect.";

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
