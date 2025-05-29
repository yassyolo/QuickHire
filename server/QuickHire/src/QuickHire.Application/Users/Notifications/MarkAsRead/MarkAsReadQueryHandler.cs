using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Notifications.MarkAsRead;

public class MarkAsReadQueryHandler : IQueryHandler<MarkAsReadQuery, Unit>
{
    private readonly INotificationService _notificationService;
    private readonly IRepository _repository;

    public MarkAsReadQueryHandler(INotificationService notificationService, IRepository repository)
    {
        _notificationService = notificationService;
        _repository = repository;
    }

    public async Task<Unit> Handle(MarkAsReadQuery request, CancellationToken cancellationToken)
    {
        var notification = await _repository.GetByIdAsync<Domain.Users.Notification, int>(request.Id);
        if (notification == null)
        {
            throw new NotFoundException(nameof(Domain.Users.Notification), request.Id);
        }

        if (notification.IsRead)
        {
            throw new InvalidOperationException("Notification is already marked as read.");
        }

        await _notificationService.MarkNotificationAsRead(request.Id);

        return Unit.Value;
    }
}

