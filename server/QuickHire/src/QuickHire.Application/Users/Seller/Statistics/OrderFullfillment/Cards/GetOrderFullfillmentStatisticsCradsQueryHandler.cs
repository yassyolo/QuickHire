using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Orders.Enums;

namespace QuickHire.Application.Users.Seller.Statistics.OrderFullfillment.Cards;

public class GetOrderFullfillmentStatisticsCradsQueryHandler : IQueryHandler<GetOrderFullfillmentStatisticsCradsQuery, IEnumerable<CardItemModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetOrderFullfillmentStatisticsCradsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<CardItemModel>> Handle(GetOrderFullfillmentStatisticsCradsQuery request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var ordersQueryable = _repository.GetAllIncluding<Order>(x => x.Reviews).Where(x => x.SellerId == sellerId);

        var ordersList = await _repository.ToListAsync(ordersQueryable);
        var completedOrders = ordersList.Where(x => x.Status == OrderStatus.Delivered);

        var revenue = completedOrders.Sum(x => x.TotalPrice);

        var deliveredReviews = completedOrders.SelectMany(x => x.Reviews);

        var averageRating = deliveredReviews.Any()? deliveredReviews.Average(x => x.Rating): 0; 

        return new List<CardItemModel>
    {
        new CardItemModel
        {
            Title = "Completed orders",
            Value = completedOrders.Count().ToString()
        },
        new CardItemModel
        {
            Title = "Sales",
            Value = ordersList.Count().ToString()
        },
        new CardItemModel
        {
            Title = "Average Rating",
            Value = averageRating.ToString("F1")
        },
        new CardItemModel
        {
            Title = "Revenue",
            Value = revenue.ToString("C")
        }
    };
    }

}


