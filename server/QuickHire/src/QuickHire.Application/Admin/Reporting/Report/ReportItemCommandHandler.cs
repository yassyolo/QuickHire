using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Moderation;
using QuickHire.Domain.Users;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength;
using System.Runtime.InteropServices;

namespace QuickHire.Application.Admin.Reporting.Report;

public class ReportItemCommandHandler : ICommandHandler<ReportItemCommand, Unit>
{
    private readonly IUserService _userService;
    private readonly IRepository _repository;
    private readonly INotificationService _notificationService;

    public ReportItemCommandHandler(IUserService userService, IRepository repository, INotificationService notificationService)
    {
        _userService = userService;
        _repository = repository;
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(ReportItemCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _userService.GetCurrentUserIdAsync();
        var newReportedItem = new ReportedItem
        {
            Reason = request.Reason,
            CreatedAt = DateTime.Now,
            ReportedById = currentUserId
        };
        if (request.SellerId.HasValue)
        {

            var reportedUserId = await _userService.GetUserIdBySellerIdAsync(request.SellerId.Value);
            newReportedItem.ReportedUserId = reportedUserId;
            await _userService.ReportUserAsync(reportedUserId);

            await _notificationService.MakeNotification(request.SellerId.Value, NotificationRecipientType.Seller,Domain.Users.Enums.NotificationType.ReportedUser,
    new Dictionary<string, string>{ { "ReportReason", request.Reason }    }); }
        else if (request.GigId.HasValue)
        {
            newReportedItem.GigId = request.GigId.Value;

            var gig = await _repository.GetByIdAsync<Domain.Gigs.Gig, int>(request.GigId.Value);
            gig.ModerationStatus = Domain.Moderation.Enums.ModerationStatus.PendingReview;

            await _repository.UpdateAsync(gig);
            await _repository.SaveChangesAsync();

            await _notificationService.MakeNotification(gig.SellerId, NotificationRecipientType.Seller, Domain.Users.Enums.NotificationType.ReportedGig,
            new Dictionary<string, string> { { "GigTitle", gig.Title }, { "ReportReason", request.Reason } });
        }

        await _repository.AddAsync(newReportedItem);
        await _repository.SaveChangesAsync();

        return Unit.Value;

    }
}
