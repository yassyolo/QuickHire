using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Messaging;

namespace QuickHire.Application.Users.Messaging.GetMessagesForConversation;

public record GetMessagesForConversationQuery(int Id) : IQuery<List<MessagesForConversationModel>>;
