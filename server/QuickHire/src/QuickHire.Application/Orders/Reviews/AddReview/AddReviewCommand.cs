using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Orders.Ratings.Reviews;

public record AddReviewCommand(int OrderId, int Rating, string Comment) : ICommand<Unit>;

