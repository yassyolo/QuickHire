using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class OrderStatusUpdateNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.OrderStatusUpdate;

    public string Title { get; set; } = "Your Order Status Has Been Updated!";
    public string Message { get; set; } = "Hello, {UserName}! The status of your order with number {OrderNumber} has changed to {NewStatus}. Check out details here: {OrderId}.";

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
