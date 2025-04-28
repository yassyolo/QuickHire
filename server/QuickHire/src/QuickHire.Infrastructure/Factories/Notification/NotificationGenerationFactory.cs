namespace QuickHire.Infrastructure.Factories.Notification;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Users.Enums;

internal class NotificationGenerationFactory : INotificationGeneratorFactory
{
    private readonly Dictionary<NotificationType, INotificationGenerator> _notificationGenerators;

    public NotificationGenerationFactory(Dictionary<NotificationType, INotificationGenerator> notificationGenerators)
    {
        _notificationGenerators = notificationGenerators;
    }

    public INotificationGenerator GetNotificationGenerator(NotificationType type)
    {
        if(_notificationGenerators.TryGetValue(type, out var generator))
        {
            return generator;
        }
        return null;
    }
}
