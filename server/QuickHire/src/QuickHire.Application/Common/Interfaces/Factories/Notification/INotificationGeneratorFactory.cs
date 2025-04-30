using QuickHire.Domain.Users.Enums;

namespace QuickHire.Application.Common.Interfaces.Factories.Notification;

public interface INotificationGeneratorFactory
{
    INotificationGenerator GetNotificationGenerator(NotificationType type);
}
