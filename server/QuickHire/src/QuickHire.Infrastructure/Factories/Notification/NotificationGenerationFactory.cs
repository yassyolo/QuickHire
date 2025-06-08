namespace QuickHire.Infrastructure.Factories.Notification;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users.Enums;

internal class NotificationGenerationFactory : INotificationGeneratorFactory
{
    private readonly Dictionary<NotificationType, INotificationGenerator> _notificationGenerators;

    public NotificationGenerationFactory(Dictionary<NotificationType, INotificationGenerator> notificationGenerators)
    {
        _notificationGenerators = notificationGenerators ?? throw new ArgumentNullException(nameof(notificationGenerators));
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
