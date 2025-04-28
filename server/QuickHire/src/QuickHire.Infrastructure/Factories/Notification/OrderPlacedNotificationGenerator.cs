using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class OrderPlacedNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.OrderPlaced;

    public string Title { get; set; } = "Your Order Has Been Placed!";
    public string Message { get; set; } = "Hello, {UserName}! Your order with number {OrderNumber} has been successfully placed. View its details here: {OrderId}.";

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
