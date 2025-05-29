using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Notifications.MarkAsRead;

public record MarkAsReadQuery(int Id) : IQuery<Unit>;

