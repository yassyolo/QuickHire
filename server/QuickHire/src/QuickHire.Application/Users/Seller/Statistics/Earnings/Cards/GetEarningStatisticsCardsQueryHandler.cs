using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.Earnings.Cards;

public class GetEarningStatisticsCardsQueryHandler : IQueryHandler<GetEarningStatisticsCardsQuery, IEnumerable<CardItemModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetEarningStatisticsCardsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<CardItemModel>> Handle(GetEarningStatisticsCardsQuery request, CancellationToken cancellationToken)
    {
        /*var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var ordersQuery = _repository.GetAllReadOnly<QuickHire.Domain.Orders.Order>().Where(x => x.SellerId == sellerId);

        var orders = await _repository.ToListAsync<QuickHire.Domain.Orders.Order>(ordersQuery);

        var toralRevenue = orders.Sum(x => x.TotalPrice);
        var completedOrdersRevenue = orders.Where(x => x.Status == QuickHire.Domain.Orders.Enums.OrderStatus.Delivered).Sum(x => x.TotalPrice);
        var inProgressOrdersRevenue = orders.Where(x => x.Status == QuickHire.Domain.Orders.Enums.OrderStatus.InProgress).Sum(x => x.TotalPrice);
        var averageOrderValue = toralRevenue / (orders.Count() == 0 ? 1 : orders.Count()); 
        return new List<CardItemModel>
        {
            new CardItemModel
            {
                Title = "Total Revenue",
                Value = toralRevenue.ToString("C")
            },
            new CardItemModel
            {
                Title = "Completed revenue",
                Value = completedOrdersRevenue.ToString("C")
            },
            new CardItemModel
            {
                Title = "Average Order Value",
                Value = averageOrderValue.ToString("C")
            },
            new CardItemModel
            {
                Title = "In-progress revenue",
                Value = inProgressOrdersRevenue.ToString("C")
            }
        };*/

        return new List<CardItemModel>
        {
            new CardItemModel
            {
                Title = "Revenue",
                Value = "8200"
            },
            new CardItemModel
            {
                Title = "Completed revenue",
                Value = "6500"
            },
            new CardItemModel
            {
                Title = "Average order value",
                Value = "164"
            },
            new CardItemModel
            {
                Title = "In-progress revenue",
                Value = "1700"
            }
        };
    }
}
