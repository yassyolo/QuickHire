using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.CustomOffers;
using QuickHire.Application.Users.Models.Messaging;
using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.CustomOffers.Enums;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Orders;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Seller.CustomOffers.CreateCustomOffer;

public class CreateCustomOfferCommandHandler : ICommandHandler<CreateCustomOfferCommand, CustomOfferReturnModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;
    private readonly INotificationService _notificationService;

    public CreateCustomOfferCommandHandler(IRepository repository, IUserService userService, INotificationService notificationService)
    {
        _repository = repository;
        _userService = userService;
        _notificationService = notificationService;
    }

    public async Task<CustomOfferReturnModel> Handle(CreateCustomOfferCommand request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var projectBrief = await _repository.GetByIdAsync<ProjectBrief, int>(request.ProjectBriefId);
        var customOffer = new CustomOffer
        {
            CustomOfferNumber = $"{"CO" + DateTime.Now.ToString("yyyyMMddHHmmss")}",
            Description = request.Description,
            Price = request.Total,
            Revisions = request.InclusivesIds.Length,
            DeliveryTimeInDays = request.DeliveryTime,
            BuyerId = projectBrief.BuyerId,
            SellerId = sellerId,
            GigId = request.GigId,
            Status = CustomOfferStatus.Pending,
            CreatedAt = DateTime.Now,
            ProjectBriefId = request.ProjectBriefId
        };

        await _repository.AddAsync(customOffer);
        await _repository.SaveChangesAsync();

        var inclusiveServicesQueryable = _repository.GetAllReadOnly<PaymentPlanInclude>().Where(x => request.InclusivesIds.Contains(x.Id));
        var inclusiveServicesList = await _repository.ToListAsync<PaymentPlanInclude>(inclusiveServicesQueryable);

        foreach (var inclusiveService in inclusiveServicesList)
        {
            inclusiveService.CustomOfferId = customOffer.Id;
        }

        var sellerIdAndMode = _userService.GetCurrentUserIdAndMode();
        var buyerUserId = await _userService.GetUserIdByBuyerIdAsync(projectBrief.BuyerId);
        var conversationId = 0;
        var existingConversationQueryable = _repository.GetAllIncluding<Conversation>().Where(x => (x.ParticipantBId == sellerIdAndMode.UserId && x.ParticipantAId == buyerUserId) || (x.ParticipantAId == sellerIdAndMode.UserId && x.ParticipantBId == buyerUserId));
        var existingConversation = await _repository.FirstOrDefaultAsync<Conversation>(existingConversationQueryable);
        if (existingConversation != null)
        {
            conversationId = existingConversation.Id;
        }
        else
        {
            var newConversation = new Conversation
            {
                ParticipantAId = sellerIdAndMode.UserId,
                ParticipantAMode = sellerIdAndMode.Mode,
                ParticipantBId = buyerUserId,
                ParticipantBMode = sellerIdAndMode.Mode == "seller" ? "buyer" : "seller",
                CreatedAt = DateTime.Now,
                LastMessageAt = DateTime.Now,
                IsStarredByParticipantA = false,
                IsStarredByParticipantB = false,
            };

            await _repository.AddAsync(newConversation);
            await _repository.SaveChangesAsync();
            conversationId = newConversation.Id;
        }

        var gig = await _repository.GetByIdAsync<Gig, int>(request.GigId);
        var username = await _userService.GetCurrentUserAsync();
            public string Message { get; set; } = "Hello, {UserName}! You have received new custom offer with numberL {CustomOfferNumber}. Check out your messages with {SellerUserName}!";

    await _notificationService.MakeNotification(projectBrief.BuyerId, Common.Interfaces.Factories.Notification.NotificationRecipientType.Buyer, Domain.Users.Enums.NotificationType.CustomOfferReceived, new Dictionary<string, string> { { "UserName", buyer.UserName! }, { "ProjectTitle", projectBrief.ProjectBriefNumber } });


        return new CustomOfferReturnModel
        {
            Text = "Custom offer details",
            ConversationId = conversationId,
            Payload = new CustomOfferPayloadModel
            {
                GigTitle = gig.Title,
                GigId = gig.Id,
                OfferAmount = customOffer.Price.ToString("F2"),
                Includes = inclusiveServicesList.Select(x => x.Name).ToList(),
                OfferId = customOffer.Id,
                SenderUsername = username.UserName ?? string.Empty
            }
        };
    }
}
