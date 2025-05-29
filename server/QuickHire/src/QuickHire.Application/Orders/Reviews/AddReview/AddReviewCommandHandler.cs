using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Orders.Ratings.Reviews;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Orders.Reviews.AddRating;

public class AddReviewCommandHandler : ICommandHandler<AddReviewCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public AddReviewCommandHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Unit> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync<Order, int>(request.OrderId);
        if (order == null)
        {
            throw new NotFoundException(nameof(Order), request.OrderId);
        }

        var user = await _userService.GetCurrentUserAsync();

        var review = new Review
        {
            OrderId = order.Id,
            CreatorUserId = user.Id,
            Rating = request.Rating,
            Comment = request.Comment,
            CreatedOn = DateTime.Now
        };

        var buyerUserId = await _userService.GetUserIdByBuyerIdAsync(order.BuyerId);
        var sellerUserId = await _userService.GetUserIdBySellerIdAsync(order.SellerId);
        review.ReceiverUserId = sellerUserId == user.Id ? buyerUserId : sellerUserId;

        await _repository.AddAsync(review);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

