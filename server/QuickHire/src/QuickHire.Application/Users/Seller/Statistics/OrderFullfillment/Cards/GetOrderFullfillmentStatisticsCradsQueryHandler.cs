using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;

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
        /*var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var ordersQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Orders.Order>().Where(x => x.SellerId == sellerId);
        ordersQueryable = _repository.GetAllIncluding<QuickHire.Domain.Orders.Order>(x => x.Reviews);
        var ordersList = await _repository.ToListAsync<QuickHire.Domain.Orders.Order>(ordersQueryable);

        var revenue = ordersList.Where(x => x.Status == QuickHire.Domain.Orders.Enums.OrderStatus.Delivered).Sum(x => x.TotalPrice);
        var averageRating = ordersList.Where(x => x.Status == QuickHire.Domain.Orders.Enums.OrderStatus.Delivered).SelectMany(x => x.Reviews).Average(x => x.Rating);

        return new List<CardItemModel>
        {
            new CardItemModel
            {
                Title = "Completed orders",
                Value = ordersList.Where(x => x.Status == QuickHire.Domain.Orders.Enums.OrderStatus.Delivered).Count().ToString()
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
        };*/

        return new List<CardItemModel>
        {
            new CardItemModel
            {
                Title = "Completed orders",
                Value = "32"
            },
            new CardItemModel
            {
                Title = "Sales",
                Value = "34"
            },
            new CardItemModel
            {
                Title = "Average rating",
                Value = "4.5"
            },
            new CardItemModel
            {
                Title = "Revenue",
                Value = "2222"
            }
        };
    }
}

