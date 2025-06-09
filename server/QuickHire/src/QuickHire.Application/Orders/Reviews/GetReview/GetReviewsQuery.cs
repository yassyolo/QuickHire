using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Orders.Models.Reviews;

namespace QuickHire.Application.Orders.Reviews.GetRatings;

public record GetReviewsQuery(int? GigId, int? UserId, string? SortBy, bool? ShowMore) : IQuery<List<ReviewResponseRowModel>>;

