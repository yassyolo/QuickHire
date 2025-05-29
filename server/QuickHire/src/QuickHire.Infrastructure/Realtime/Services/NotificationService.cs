using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Notifications;
using QuickHire.Domain.Users;
using QuickHire.Domain.Users.Enums;
using QuickHire.Infrastructure.Realtime.Hubs;

namespace QuickHire.Infrastructure.Realtime.Services;

internal class NotificationService : INotificationService
{
    private readonly IRepository _repository;
    private readonly INotificationGeneratorFactory _notificationGeneratorFactory;

    public NotificationService(INotificationGeneratorFactory notificationGeneratorFactory, IRepository repository)
    {
        _notificationGeneratorFactory = notificationGeneratorFactory;
        _repository = repository;
    }

    public async Task MakeNotification(string userId, NotificationType type, Dictionary<string, string>? placeholders = null)
    {
        var notificationGenerator = _notificationGeneratorFactory.GetNotificationGenerator(type);
        var notification = notificationGenerator.Generate(userId, placeholders);
        await _repository.AddAsync(notification);
        await _repository.SaveChangesAsync();
    }

    public async Task MarkNotificationAsRead(int id)
    {
        var notification = await _repository.GetByIdAsync<Notification, int>(id);

        if (notification != null)
        {
            notification!.IsRead = true;
            await _repository.UpdateAsync(notification);
            await _repository.SaveChangesAsync();
        }
    } 
    
    public async Task<IEnumerable<Notification>> GetUserNotifications(string userId)
    {
        return await _repository.GetAllReadOnly<Notification>().Where(x => !x.IsRead && x.UserId== userId && x.Sent == false).ToListAsync();
    }
}
