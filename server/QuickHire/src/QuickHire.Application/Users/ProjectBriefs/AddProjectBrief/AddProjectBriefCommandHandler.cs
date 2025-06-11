using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.ProjectBriefs.Enums;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.ProjectBriefs.AddProjectBrief;

public class AddProjectBriefCommandHandler : ICommandHandler<AddProjectBriefCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;
    private readonly INotificationService _notificationService;
    private readonly IGigScoringService _gigScoringService;

    public AddProjectBriefCommandHandler(IRepository repository, IUserService userService, INotificationService notificationService, IGigScoringService gigScoringService)
    {
        _repository = repository;
        _userService = userService;
        _notificationService = notificationService;
        _gigScoringService = gigScoringService;
    }

    public async Task<Unit> Handle(AddProjectBriefCommand request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();

        var projectBrief = new ProjectBrief
        {
            ProjectBriefNumber = $"{"PB" + DateTime.Now.ToString("yyyyMMddHHmmss")}",
            Description = request.Description,
            AboutBuyer = request.AboutBuyer,
            Budget = request.Budget,
            DeliveryTimeInDays = request.DeliveryDays,
            BuyerId = buyerId,
            SubSubCategoryId = request.SubSubCategoryId,
            CreatedAt = DateTime.Now,
            Status = ProjectBriefStatus.Pending,
        };

        await _repository.AddAsync(projectBrief);
        await _repository.SaveChangesAsync();

        var buyer = await _userService.GetCurrentUserAsync();


        await _notificationService.MakeNotification(buyerId, Common.Interfaces.Factories.Notification.NotificationRecipientType.Buyer, Domain.Users.Enums.NotificationType.NewProjectBriefMade, new Dictionary<string, string> {{ "UserName", buyer.UserName! }, { "ProjectTitle", projectBrief.ProjectBriefNumber }    });

        var gigScore = await _gigScoringService.GetTopScoringGigsAsync(buyerId, request.AboutBuyer, request.Description, request.SubSubCategoryId, request.Budget, request.DeliveryDays);

        foreach (var gig in gigScore)
        {
            var suitableSeller = new SuitableSellerProjectBrief
            {
                ProjectBriefId = projectBrief.Id,
                SellerId = gig.SellerId,
                CreatedAt = DateTime.Now,
            };

            await _repository.AddAsync(suitableSeller);

            await _notificationService.MakeNotification(gig.SellerId, Common.Interfaces.Factories.Notification.NotificationRecipientType.Seller, Domain.Users.Enums.NotificationType.ProjectBriefReceived, new Dictionary<string, string> { { "UserName", buyer.UserName! }, { "ProjectBriefNumber", projectBrief.ProjectBriefNumber } });
        }

        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

