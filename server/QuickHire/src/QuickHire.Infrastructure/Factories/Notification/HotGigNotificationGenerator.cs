﻿using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Infrastructure.Extensions.PlaceholderExtension;

namespace QuickHire.Infrastructure.Factories.Notification;

internal class HotGigNotificationGenerator : INotificationGenerator
{
    public NotificationType Type => NotificationType.HotGig;

    public string Title { get; set; } = "Your Gig is Getting Hot!";
    public string Message { get; set; } = "Hello, {UserName}! Your gig titled '{GigTitle}' is gaining popularity. You’re getting more views and attention! Check it out and keep up the great work: {GigId}.";

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