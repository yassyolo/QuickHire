using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class CustomOfferCancelled: INotificationGenerator
{
    public NotificationType Type => NotificationType.CustomOfferCancelled;

    public string Title { get; set; } = "Your Custom Offer Has Been Cancelled";
    public string Message { get; set; } = "Hello, {UserName}. We're sorry to inform you that your custom offer with number {CustomOfferNumber} has been cancelled. Click here to see why: {CustomOfferId}";

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
