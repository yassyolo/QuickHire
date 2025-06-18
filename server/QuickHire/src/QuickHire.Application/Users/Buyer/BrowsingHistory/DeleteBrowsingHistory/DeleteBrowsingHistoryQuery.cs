using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Buyer.BrowsingHistory.DeleteBrowsingHistory;

public record DeleteBrowsingHistoryQuery : IQuery<Unit>;
