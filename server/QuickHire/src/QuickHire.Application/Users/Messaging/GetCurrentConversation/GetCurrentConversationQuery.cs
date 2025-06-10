using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Messaging;

namespace QuickHire.Application.Users.Messaging.GetCurrentConversation;

public record GetCurrentConversationQuery(int Id) : IQuery<CurrentConversationModel>;
