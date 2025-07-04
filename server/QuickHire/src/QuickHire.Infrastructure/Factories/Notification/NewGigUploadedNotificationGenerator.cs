﻿using iText.Commons.Bouncycastle.Cms;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class NewGigUploadedNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.NewGigUploaded;

    public string Title { get; set; } = "A New Gig Has Been Uploaded!";
    public string Message { get; set; } = "Hello, {UserName}! Your new gig titled '{GigTitle}' has been successfully uploaded. Click here to review it or make any changes: {GigId}.";

    public Domain.Users.Notification Generate(int recipientId, NotificationRecipientType recipientType, Dictionary<string, string>? placeholders = null)
    {
        string finalTitle = ReplacePlaceHolders(Title, placeholders);
        string finalMessage = ReplacePlaceHolders(Message, placeholders);

        var notification = new Domain.Users.Notification
        {
            CreatedAt = DateTime.Now,
            IsRead = false,
            Title = finalTitle,
            Message = finalMessage,
            Sent = false
        };

        if (recipientType == NotificationRecipientType.Buyer)
            notification.BuyerId = recipientId;
        else if (recipientType == NotificationRecipientType.Seller)
            notification.SellerId = recipientId;

        return notification;
    }
}
