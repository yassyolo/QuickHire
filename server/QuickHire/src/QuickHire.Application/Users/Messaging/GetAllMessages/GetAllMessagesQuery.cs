using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Messaging;

namespace QuickHire.Application.Users.Messaging.GetAllMessages;

public record GetAllMessagesQuery(int[]? OrderStatusIds, bool? HasCustomOffer, bool? IsStarred) : IQuery<List<GetAllMessagesItemModel>>;
