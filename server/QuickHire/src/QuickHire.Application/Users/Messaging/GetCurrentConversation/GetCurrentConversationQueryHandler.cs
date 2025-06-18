using Microsoft.VisualBasic;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Messaging;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Messaging.Enums;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

namespace QuickHire.Application.Users.Messaging.GetCurrentConversation;

public class GetCurrentConversationQueryHandler : IQueryHandler<GetCurrentConversationQuery, CurrentConversationModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetCurrentConversationQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<CurrentConversationModel> Handle(GetCurrentConversationQuery request, CancellationToken cancellationToken)
    {
        var currentUserIdAndMode = _userService.GetCurrentUserIdAndMode();

        var messagesQueryable = _repository.GetAllIncluding<Message>(x => x.Conversation).Where(x => x.Id == request.Id && x.Conversation.Order ==null);
        var message = await _repository.FirstOrDefaultAsync(messagesQueryable);
       
        message.IsRead = true;
        await _repository.UpdateAsync(message);

       if ((message.Conversation.ParticipantAId != currentUserIdAndMode.UserId || message.Conversation.ParticipantAMode != currentUserIdAndMode.Mode) &&
                       (message.Conversation.ParticipantBId != currentUserIdAndMode.UserId || message.Conversation.ParticipantBMode != currentUserIdAndMode.Mode))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this conversation.");
            }    

        var conversationQueryable = _repository.GetAllIncluding<Conversation>(x => x.Messages).Where(x => x.Id == message.Conversation.Id);
        var conversation = await _repository.FirstOrDefaultAsync(conversationQueryable);
        var messages = conversation?.Messages?.OrderBy(x => x.SentAt).ToList() ?? new List<Message>();    

        var participantInfo = message.Conversation.ParticipantAId == currentUserIdAndMode.UserId && message.Conversation.ParticipantAMode == currentUserIdAndMode.Mode
            ? await _userService.GetParticipantInfoAsync(message.Conversation.ParticipantBId)
            : await _userService.GetParticipantInfoAsync(message.Conversation.ParticipantAId);

        var participantBInfo = new ParticipantBInfoModel
        {
            Id = participantInfo.Id,
            ProfilePictureUrl = participantInfo.ProfilePictureUrl,
            FullName = participantInfo.FullName,
            Country = participantInfo.Country,
            Username = participantInfo.Username,
            Languages = participantInfo.Languages ?? Array.Empty<string>(),
            MemberSince = participantInfo.MemberSince
        };

        var result = new CurrentConversationModel
        {
            Id = message.Conversation.Id,
           
            IsStarred = message.Conversation.ParticipantAId == currentUserIdAndMode.UserId ? message.Conversation.IsStarredByParticipantA : message.Conversation.IsStarredByParticipantB,        
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


        var ordersQueryable = _repository.GetAllIncluding<Order>(x => x.Conversation, x => x.Gig, x => x.SelectedPaymentPlan).Where(x => x.Conversation.Id == conversation.Id);
        var order = await _repository.FirstOrDefaultAsync(ordersQueryable);
        if (order != null)
        {
            var dueTimeSpan = order.CreatedAt
    .AddDays(order.SelectedPaymentPlan.DeliveryTimeInDays) - DateTime.UtcNow;

            string formattedDue = $"{(int)dueTimeSpan.TotalDays} days {dueTimeSpan.Hours} hours {dueTimeSpan.Minutes} minutes";
            result.CurrentOrder = new CurrentOrderModel
            {
                Id = order.Id,
                Price = order.TotalPrice.ToString(),
                DueOn = formattedDue,
                Status = order.Status.ToString(),
               ImageUrl = order.Gig.ImageUrls.FirstOrDefault() ?? string.Empty,
            };
        }

        result.Messages = messagesModel;
        result.ParticipantBInfo = participantBInfo;

        return result;

       
    }

}

