using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Orders.Models.Reviews;

namespace QuickHire.Application.Orders.Reviews.GetRatings;

public record GetReviewsQuery(int? GigId, string? UserId, string? SortBy, bool GetAll = false) : IQuery<List<ReviewResponseRowModel>>;

