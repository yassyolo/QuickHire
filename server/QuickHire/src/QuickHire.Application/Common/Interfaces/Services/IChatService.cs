using QuickHire.Application.Users.Models.Messaging;

namespace QuickHire.Application.Common.Interfaces.Services;

public interface IChatService
{
    Task<MessagesForConversationModel> CreateNewMessage(NewMessageModel newMessageModel);
}
