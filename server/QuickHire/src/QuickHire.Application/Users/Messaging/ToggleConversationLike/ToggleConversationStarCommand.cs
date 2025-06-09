using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Messaging.ToggleConversationLike;

public record ToggleConversationStarCommand(int MessageId) : ICommand<Unit>;
