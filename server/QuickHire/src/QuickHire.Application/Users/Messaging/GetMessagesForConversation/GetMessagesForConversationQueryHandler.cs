using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Messaging;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Shared.Exceptions;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

namespace QuickHire.Application.Users.Messaging.GetMessagesForConversation;

public class GetMessagesForConversationQueryHandler : IQueryHandler<GetMessagesForConversationQuery, List<MessagesForConversationModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetMessagesForConversationQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<List<MessagesForConversationModel>> Handle(GetMessagesForConversationQuery request, CancellationToken cancellationToken)
    {
        var currentUserIdAndMode = _userService.GetCurrentUserIdAndMode();

        var conversationsQueryable = _repository.GetAllIncluding<Conversation>(x => x.Messages).Where(x => x.Id == request.Id);
        var conversation = await _repository.FirstOrDefaultAsync(conversationsQueryable);
        if (conversation == null)
        {
            throw new NotFoundException(nameof(Conversation), "Conversation not found for the given message ID.");
        }

        if ((conversation.ParticipantAId != currentUserIdAndMode.UserId || conversation.ParticipantAMode != currentUserIdAndMode.Mode) &&
                       (conversation.ParticipantBId != currentUserIdAndMode.UserId || conversation.ParticipantBMode != currentUserIdAndMode.Mode))
        {
            throw new UnauthorizedAccessException("You do not have permission to access this conversation.");
        }

        var messages = conversation.Messages.OrderBy(x => x.SentAt).ToList();

        var result = new List<MessagesForConversationModel>();
        foreach (var message in messages)
        {
            var getSender = message.SenderId == conversation.ParticipantAId && message.SenderId == conversation.ParticipantAMode
                ? await _userService.GetUsernameAndProfilePictureAsync(conversation.ParticipantAId)
                : await _userService.GetUsernameAndProfilePictureAsync(conversation.ParticipantBId);

            var payload = message.PayloadJson != null ? System.Text.Json.JsonSerializer.Deserialize<CustomOfferPayloadModel>(message.PayloadJson) : null;
            var model = new MessagesForConversationModel
            {
                Id = message.Id,
                SenderProfilePictureUrl = getSender.ProfilePictureUrl,
                Content = message.Text,
                Timestamp = message.SentAt.ToString("dd-MM"), 
                SenderUsername = getSender.Username,
                MessageType = message.Type.ToString().ToLower(),
                Payload = payload,
                FileUrl = message.AttachmentUrl != null ? message.AttachmentUrl : null
            };
            result.Add(model);
        }

        return result;
    }

}

