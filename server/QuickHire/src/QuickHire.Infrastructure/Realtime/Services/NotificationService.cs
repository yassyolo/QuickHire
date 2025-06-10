using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Notifications;
using QuickHire.Domain.Users;
using QuickHire.Domain.Users.Enums;

namespace QuickHire.Infrastructure.Realtime.Services;

internal class NotificationService : INotificationService
{
    private readonly IRepository _repository;
    private readonly INotificationGeneratorFactory _notificationGeneratorFactory;
    private readonly IUserService _userService;

    public NotificationService(INotificationGeneratorFactory notificationGeneratorFactory, IRepository repository)
    {
        _notificationGeneratorFactory = notificationGeneratorFactory;
        _repository = repository;
    }

    public async Task MakeNotification(int recipientId, NotificationRecipientType recipientType, NotificationType type, Dictionary<string, string>? placeholders = null)
    {
        var notificationGenerator = _notificationGeneratorFactory.GetNotificationGenerator(type);
        var notification = notificationGenerator.Generate(recipientId, recipientType, placeholders);
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
    
    public async Task<IEnumerable<Notification>> GetUserNotifications(bool buyer)
    {
        if(buyer)
        {
            var buyerId = await _userService.GetBuyerIdByUserIdAsync();
            return await _repository.GetAllReadOnly<Notification>().Where(x => x.BuyerId == buyerId && x.Sent == false).ToListAsync();
        }
        else
        {
            var sellerId = await _userService.GetSellerIdByUserIdAsync();
            return await _repository.GetAllReadOnly<Notification>().Where(x => x.SellerId == sellerId && x.Sent == false).ToListAsync();
        }
    }
}
