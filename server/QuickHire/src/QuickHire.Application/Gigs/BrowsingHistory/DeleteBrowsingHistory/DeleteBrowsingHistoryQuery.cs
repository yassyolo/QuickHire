using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Gigs.BrowsingHistory.DeleteBrowsingHistory;

public record DeleteBrowsingHistoryQuery : IQuery<Unit>;
