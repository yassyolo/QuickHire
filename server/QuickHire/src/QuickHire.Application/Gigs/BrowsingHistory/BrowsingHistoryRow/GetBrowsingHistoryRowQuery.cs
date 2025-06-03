using QuickHire.Application.Admin.Models.Gigs;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Gigs.BrowsingHistory.BrowsingHistoryRow;

public record GetBrowsingHistoryRowQuery() : IQuery<IEnumerable<BrowsingHistoryRowModel>>;
