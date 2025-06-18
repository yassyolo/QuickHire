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
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;

namespace QuickHire.Application.CustomOffers.Seller.CreateCustomOffer;

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
        if (projectBrief == null)
        {
            throw new NotFoundException(nameof(ProjectBrief), request.ProjectBriefId);
        }

        try
        {
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
            var inclusiveServicesList = await _repository.ToListAsync(inclusiveServicesQueryable);

            foreach (var inclusiveService in inclusiveServicesList)
            {
                var customOfferInclude = new CustomOfferInclusives
                {
                    CustomOfferId = customOffer.Id,
                    PaymentPlanIncludeId = inclusiveService.Id,
                };

                await _repository.AddAsync(customOfferInclude);
            }
            await _repository.SaveChangesAsync();

            var sellerUserIdAndMode = _userService.GetCurrentUserIdAndMode();
            var buyerUserId = await _userService.GetUserIdByBuyerIdAsync(projectBrief.BuyerId);
            var conversationId = 0;
            var existingConversationQueryable = _repository.GetAllIncluding<Conversation>().Where(x => x.ParticipantBId == sellerUserIdAndMode.UserId && x.ParticipantAId == buyerUserId && x.ParticipantAMode == "buyer" && x.ParticipantBMode == sellerUserIdAndMode.Mode || x.ParticipantAId == sellerUserIdAndMode.UserId && x.ParticipantAMode == sellerUserIdAndMode.Mode && x.ParticipantBId == buyerUserId && x.ParticipantBMode == "buyer");
            var existingConversation = await _repository.FirstOrDefaultAsync(existingConversationQueryable);
            if (existingConversation != null)
            {
                conversationId = existingConversation.Id;
            }
            else
            {
                var newConversation = new Conversation
                {
                    ParticipantAId = sellerUserIdAndMode.UserId,
                    ParticipantAMode = sellerUserIdAndMode.Mode,
                    ParticipantBId = buyerUserId,
                    ParticipantBMode = "buyer",
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
            var senderUsername = await _userService.GetUsernameByUserIdAsync(sellerUserIdAndMode.UserId);

            await _notificationService.MakeNotification(projectBrief.BuyerId, Common.Interfaces.Factories.Notification.NotificationRecipientType.Buyer, Domain.Users.Enums.NotificationType.CustomOfferReceived, new Dictionary<string, string> { { "SellerUserName", senderUsername } });

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
                    SenderUsername = senderUsername 
                }
            };
        }
        catch (Exception ex)
        {
            throw new BadRequestException("Failed to create custom offer.", ex.Message);
        }             
    }
}
