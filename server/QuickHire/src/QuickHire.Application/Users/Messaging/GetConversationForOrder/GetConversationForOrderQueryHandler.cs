using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Messaging;
using QuickHire.Domain.Messaging.Enums;
using QuickHire.Domain.Messaging;

namespace QuickHire.Application.Users.Messaging.GetConversationForOrder;

public class GetConversationForOrderQueryHandler : IQueryHandler<GetConversationForOrderQuery, CurrentConversationModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetConversationForOrderQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<CurrentConversationModel> Handle(GetConversationForOrderQuery request, CancellationToken cancellationToken)
    {
        var currentUserIdAndMode = _userService.GetCurrentUserIdAndMode();

        var conversationQueryable = _repository.GetAllIncluding<Conversation>(x => x.Messages).Where(x => x.Id == request.Id);
        var conversation = await _repository.FirstOrDefaultAsync(conversationQueryable);
        var messages = conversation?.Messages?.OrderBy(x => x.SentAt).ToList() ?? new List<Message>();       

        var result = new CurrentConversationModel
        {
            Id = conversation.Id,
        };

        var messagesModel = new List<MessagesForConversationModel>();
        foreach (var m in messages)
        {
            var getSender = await _userService.GetUsernameAndProfilePictureAsync(m.SenderId);

            object? payload = null;
            if (!string.IsNullOrEmpty(m.PayloadJson))
            {
                payload = m.Type switch
                {
                    MessageType.CustomOffer => System.Text.Json.JsonSerializer.Deserialize<CustomOfferPayloadModel>(m.PayloadJson),
                    MessageType.Revision => System.Text.Json.JsonSerializer.Deserialize<RevisionPayloadModel>(m.PayloadJson),
                    MessageType.Delivery => System.Text.Json.JsonSerializer.Deserialize<DeliveryPayloadModel>(m.PayloadJson),
                    _ => null
                };
            }

            var model = new MessagesForConversationModel
            {
                Id = m.Id,
                SenderProfilePictureUrl = getSender.ProfilePictureUrl,
                Content = m.Text,
                Timestamp = m.SentAt.ToString("dd-MM"),
                SenderUsername = getSender.Username,
                MessageType = m.Type.ToString().ToLower(),
                Payload = payload,
                FileUrl = m.AttachmentUrl
            };
            messagesModel.Add(model);
        }
        
        result.Messages = messagesModel;

        return result;
    }
}
