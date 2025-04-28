using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class OrderDeliveredNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.OrderDelivered;

    public string Title { get; set; } = "Your Order Has Been Delivered!";
    public string Message { get; set; } = "Hello, {UserName}! Your order with number {OrderNumber} has been successfully delivered. Please check and confirm the delivery: {OrderId}.";

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

