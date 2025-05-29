namespace QuickHire.Infrastructure.Factories.Notification;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users.Enums;

internal class NotificationGenerationFactory : INotificationGeneratorFactory
{
    private readonly Dictionary<NotificationType, INotificationGenerator> _notificationGenerators;

    public NotificationGenerationFactory(Dictionary<NotificationType, INotificationGenerator> notificationGenerators)
    {
        _notificationGenerators = new Dictionary<NotificationType, INotificationGenerator> {
            { NotificationType.NewProjectBriefMade, new NewProjectBriefMadeNotificationGenerator() },
            { NotificationType.CustomOfferReceived, new CustomOfferReceivedNotificationGenerator() },
            { NotificationType.CustomOfferAccepted, new CustomOfferAcceptedNotificationGenerator() },
            { NotificationType.CustomOfferCancelled, new CustomOfferCancelledNotificationGenerator() },
            { NotificationType.CustomOfferExpired, new CustomOfferExpiredNotificationGenerator() },
            { NotificationType.CustomRequestPlaced, new CustomRequestPlacedNotificationGenerator() },
            { NotificationType.CustomRequestReceived, new CustomRequestReceivedNotificationGenerator() },
            { NotificationType.HotGig, new HotGigNotificationGenerator() },
            { NotificationType.NewGigUploaded, new NewGigUploadedNotificationGenerator() },
            { NotificationType.NewProjectBriefMade, new NewProjectBriefMadeNotificationGenerator() },
            { NotificationType.OrderDelivered, new OrderDeliveredNotificationGenerator() },
            { NotificationType.OrderPlaced, new OrderPlacedNotificationGenerator() },
            { NotificationType.OrderStatusUpdate, new OrderStatusUpdateNotificationGenerator() },
            { NotificationType.ProfileMade, new ProfileMadeNotificationGenerator() },
            { NotificationType.ProfileUpdate, new ProfileUpdateNotificationGenerator() },
            { NotificationType.ProjectBriefReceived, new ProjectBriefReceivedNotificationGenerator() },
            { NotificationType.RevisionReceived, new RevisionReceivedNotificationGenerator() },           
        };
    }

    public INotificationGenerator GetNotificationGenerator(NotificationType type)
    {
        if(_notificationGenerators.TryGetValue(type, out var generator))
        {
            return generator;
        }
        throw new NotFoundException(nameof(INotificationGenerator), type);
    }
}
