using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
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
    private readonly INotificationService _notificationService;

    public AddReviewCommandHandler(IRepository repository, IUserService userService, INotificationService notificationService)
    {
        _repository = repository;
        _userService = userService;
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _repository.GetByIdAsync<Order, int>(request.OrderId);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            var userId = _userService.GetCurrentUserIdAsync();
            var review = new Review
            {
                OrderId = order.Id,
                UserId = userId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedOn = DateTime.Now
            };

            await _repository.AddAsync(review);
            await _repository.SaveChangesAsync();

            await _notificationService.MakeNotification(order.SellerId, NotificationRecipientType.Seller, Domain.Users.Enums.NotificationType.ReviewReceived, new Dictionary<string, string> { { "OrderNumber", order.OrderNumber } });
        }
        catch (Exception ex)
        {
            throw new BadRequestException("Error while adding review", ex.Message);
        }
        return Unit.Value;
    }
}

