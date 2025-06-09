using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Orders.Models.Reviews;

namespace QuickHire.Application.Orders.Reviews.RatingDistribution;

public record GetRatingDistributionQuery(int? GigId, int? UserId) : IQuery<RatingDistributionResponseModel>;
