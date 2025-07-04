﻿using iText.Commons.Bouncycastle.Cms;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class RevisionReceivedNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.RevisionReceived;

    public string Title { get; set; } = "You Have Received A New Revision!";
    public string Message { get; set; } = "Hello, {UserName}! A revision has sent for your order with number {OrderNumber}. Please review it and make the necessary feedback: {ConversationId}.";

    public Domain.Users.Notification Generate(int recipientId, NotificationRecipientType recipientType, Dictionary<string, string>? placeholders = null)
    {
        string finalTitle = ReplacePlaceHolders(Title, placeholders);
        string finalMessage = ReplacePlaceHolders(Message, placeholders);

        var notification =  new Domain.Users.Notification
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
