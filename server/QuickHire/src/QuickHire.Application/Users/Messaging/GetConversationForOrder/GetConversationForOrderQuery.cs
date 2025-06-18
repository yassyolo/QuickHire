using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Messaging;

namespace QuickHire.Application.Users.Messaging.GetConversationForOrder;

public record GetConversationForOrderQuery(int Id) : IQuery<CurrentConversationModel>;

