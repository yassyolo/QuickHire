using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Messaging;
using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Orders;

namespace QuickHire.Application.Users.Messaging.GetAllMessages;

public class GetAllMessagesQueryHandler : IQueryHandler<GetAllMessagesQuery, List<GetAllMessagesItemModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetAllMessagesQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<List<GetAllMessagesItemModel>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
    {
        var currentUserIdAndMode = _userService.GetCurrentUserIdAndModeAsync();

        var conversationsQueryable = _repository.GetAllIncluding<Conversation>(x => x.Messages)
            .Where(x => (x.ParticipantAId == currentUserIdAndMode.UserId && x.ParticipantAMode == currentUserIdAndMode.Mode) || (x.ParticipantBId == currentUserIdAndMode.UserId && x.ParticipantBMode == currentUserIdAndMode.Mode));

        if (request.IsStarred.HasValue&& request.IsStarred == true)
        {
            conversationsQueryable = conversationsQueryable.Where(x => x.ParticipantAId == currentUserIdAndMode.UserId ? x.IsStarredByParticipantA: x.IsStarredByParticipantB);
        }

        if (request.HasCustomOffer.HasValue && request.HasCustomOffer.Value)
        {
            var conversationsWithCustomOffers = _repository.GetAllIncluding<CustomOffer>(x => x.Message.Conversation)
                .Where(x =>(x.Message.Conversation.ParticipantAId == currentUserIdAndMode.UserId && x.Message.Conversation.ParticipantAMode == currentUserIdAndMode.Mode) ||(x.Message.Conversation.ParticipantBId == currentUserIdAndMode.UserId && x.Message.Conversation.ParticipantBMode == currentUserIdAndMode.Mode) )
                .Select(x => x.Message.ConversationId);

            conversationsQueryable = conversationsQueryable.Where(x => conversationsWithCustomOffers.Contains(x.Id));
        }

        if(request.OrderStatusIds != null && request.OrderStatusIds.Any())
        {
            var conversationsWithOrders = _repository.GetAllIncluding<Order>(x => x.Conversation).Where(x => (x.Conversation.ParticipantAId == currentUserIdAndMode.UserId && x.Conversation.ParticipantAMode == currentUserIdAndMode.Mode) || (x.Conversation.ParticipantBId == currentUserIdAndMode.UserId && x.Conversation.ParticipantBMode == currentUserIdAndMode.Mode))
                .Where(x => request.OrderStatusIds.Contains((int)x.Status)).Select(x => x.ConversationId);

            conversationsQueryable = conversationsQueryable.Where(x => conversationsWithOrders.Contains(x.Id));
        }

        var conversationsList = await _repository.ToListAsync(conversationsQueryable);

        var messages = conversationsList.SelectMany(x => x.Messages).OrderByDescending(x => x.SentAt).GroupBy(x => x.ConversationId).Select(x => x.FirstOrDefault()).Where(x => x != null)
            .ToList();

        var result = new List<GetAllMessagesItemModel>();
        foreach (var message in messages)
        {
            var conversation = conversationsList.FirstOrDefault(x => x.Id == message.ConversationId);
            if (conversation == null) continue;

            var sender = conversation.ParticipantAId == currentUserIdAndMode.UserId? await _userService.GetUsernameAndProfilePictureAsync(conversation.ParticipantBId)
                : await _userService.GetUsernameAndProfilePictureAsync(conversation.ParticipantAId);

            result.Add(new GetAllMessagesItemModel
            {
                Id = message.Id,
                Content = message.Text,
                Timestamp = message.SentAt.ToString("dd-MM"), 
                IsRead = message.IsRead,
                SenderUsername = sender.Username,
                SenderProfilePictureUrl = sender.ProfilePictureUrl,
                IsStarred = conversation.ParticipantAId == currentUserIdAndMode.UserId? conversation.IsStarredByParticipantA : conversation.IsStarredByParticipantB
            });
        }

        return result;


    }
}
