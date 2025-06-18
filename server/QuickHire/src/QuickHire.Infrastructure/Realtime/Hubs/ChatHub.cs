using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Messaging;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Messaging.Enums;
using QuickHire.Domain.Shared.Exceptions;
using System.Text.Json;

public class ChatHub : Hub
{
    private readonly IUserService _userService;
    private readonly IRepository _repository;
    private readonly ICloudinaryService _cloudinaryService;

    public ChatHub(IUserService userService, IRepository repository, ICloudinaryService cloudinaryService)
    {
        _userService = userService;
        _repository = repository;
        _cloudinaryService = cloudinaryService;
    }

    public class NewMessageSignalRDto
    {
        public int ConversationId { get; set; }
        public MessagesForConversationModel Message { get; set; } = default!;
        public GetAllMessagesItemModel ConversationPreview { get; set; } = default!;
    }

    public async Task SendMessage(string Text, int? ConversationId, string? AttachmentUrl, string? PayloadJson, MessageType? PayloadType, string? ReceiverId)
    {
        var senderId = _userService.GetCurrentUserIdAndMode();

        int conversationId;
        string receiverId = ReceiverId ?? string.Empty;

        if (ConversationId.HasValue)
        {
            var existingConversation = await _repository.GetAll<Conversation>()
                .FirstOrDefaultAsync(x => x.Id == ConversationId.Value)
                ?? throw new NotFoundException(nameof(Conversation), "Conversation not found.");

            conversationId = existingConversation.Id;
            receiverId = existingConversation.ParticipantAId == senderId.UserId ? existingConversation.ParticipantBId : existingConversation.ParticipantAId;
        }
        else if (!string.IsNullOrEmpty(ReceiverId))
        {
            var existingConversation = await _repository.GetAll<Conversation>()
                .Where(x => (x.ParticipantAId == senderId.UserId && x.ParticipantBId == ReceiverId) ||
                            (x.ParticipantBId == senderId.UserId && x.ParticipantAId == ReceiverId))
                .FirstOrDefaultAsync();

            if (existingConversation != null)
            {
                conversationId = existingConversation.Id;
            }
            else
            {
                var newConversation = new Conversation
                {
                    ParticipantAId = senderId.UserId,
                    ParticipantAMode = senderId.Mode,
                    ParticipantBId = receiverId,
                    ParticipantBMode = senderId.Mode == "seller" ? "buyer" : "seller",
                    CreatedAt = DateTime.UtcNow,
                    LastMessageAt = DateTime.UtcNow
                };

                await _repository.AddAsync(newConversation);
                await _repository.SaveChangesAsync();
                conversationId = newConversation.Id;
            }
        }
        else
        {
            throw new ArgumentException("Must provide either ConversationId or ReceiverId");
        }

        object? payload = null;
        if (!string.IsNullOrEmpty(PayloadJson) && PayloadType.HasValue)
        {
            payload = PayloadType switch
            {
                MessageType.CustomOffer => JsonSerializer.Deserialize<CustomOfferPayloadModel>(PayloadJson),
                MessageType.Revision => JsonSerializer.Deserialize<RevisionPayloadModel>(PayloadJson),
                _ => null
            };
        }

        var newMessageModel = new NewMessageModel
        {
            Text = Text,
            ConversationId = conversationId,
            AttachmentUrl = AttachmentUrl,
            Payload = payload,
            SenderId = senderId.UserId,
            SenderRole = senderId.Mode,
            ReceiverId = receiverId,
            ReceiverRole = senderId.Mode == "seller" ? "buyer" : "seller"
        };

        var message = await CreateNewMessage(newMessageModel);

        var preview = new GetAllMessagesItemModel
        {
            Id = conversationId,
            Content = message.Content,
            Timestamp = message.Timestamp,
            IsRead = false,
            SenderUsername = message.SenderUsername,
            SenderProfilePictureUrl = message.SenderProfilePictureUrl,
            IsStarred = false
        };

        var dto = new NewMessageSignalRDto
        {
            ConversationId = newMessageModel.ConversationId,
            Message = message,
            ConversationPreview = preview
        };

        await Clients.Group(newMessageModel.SenderId).SendAsync("ReceiveMessage", dto);
        await Clients.Group(newMessageModel.ReceiverId).SendAsync("ReceiveMessage", dto);
    }

    private async Task<MessagesForConversationModel> CreateNewMessage(NewMessageModel newMessageModel)
    {
        MessageType type;
        string? payloadJson = null;

        if (newMessageModel.Payload is CustomOfferPayloadModel customOffer)
        {
            type = MessageType.CustomOffer;
            payloadJson = JsonSerializer.Serialize(customOffer);
        }
        else if (newMessageModel.Payload is RevisionPayloadModel revision)
        {
            type = MessageType.Revision;
            payloadJson = JsonSerializer.Serialize(revision);
        }
        else if (!string.IsNullOrEmpty(newMessageModel.AttachmentUrl))
        {
            type = MessageType.FileInclude;
        }
        else
        {
            type = MessageType.Text;
        }

        var message = new Message
        {
            SenderId = newMessageModel.SenderId,
            SenderRole = newMessageModel.SenderRole,
            ReceiverId = newMessageModel.ReceiverId,
            ReceiverRole = newMessageModel.ReceiverRole,
            Text = newMessageModel.Text,
            SentAt = DateTime.UtcNow,
            IsRead = false,
            ConversationId = newMessageModel.ConversationId,
            AttachmentUrl = newMessageModel.AttachmentUrl,
            PayloadJson = payloadJson,
            Type = type
        };

        await _repository.AddAsync(message);
        await _repository.SaveChangesAsync();

        var conversation = await _repository.GetAll<Conversation>()
            .FirstOrDefaultAsync(x => x.Id == newMessageModel.ConversationId)
            ?? throw new NotFoundException(nameof(Conversation), "Conversation not found for the given message ID.");

        conversation.LastMessageAt = message.SentAt;
        await _repository.UpdateAsync(conversation);
        await _repository.SaveChangesAsync();

        var participantInfo = await _userService.GetUsernameAndProfilePictureAsync(message.SenderId);

        return new MessagesForConversationModel
        {
            Id = message.Id,
            SenderProfilePictureUrl = participantInfo.ProfilePictureUrl,
            Content = message.Text,
            Timestamp = message.SentAt.ToString("dd-MM"),
            SenderUsername = participantInfo.Username,
            MessageType = message.Type.ToString().ToLower(),
            Payload = DeserializePayload(message.PayloadJson, message.Type),
            FileUrl = message.AttachmentUrl
        };
    }

    private object? DeserializePayload(string? payloadJson, MessageType messageType)
    {
        if (string.IsNullOrEmpty(payloadJson)) return null;

        return messageType switch
        {
            MessageType.CustomOffer => JsonSerializer.Deserialize<CustomOfferPayloadModel>(payloadJson),
            MessageType.Revision => JsonSerializer.Deserialize<RevisionPayloadModel>(payloadJson),
            _ => null
        };
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }

        await base.OnDisconnectedAsync(exception);
    }
}
