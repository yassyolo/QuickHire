using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.RepeatBusiness.Cards;

public class GetRepeatBusinessStatisticsCardsQueryHandler : IQueryHandler<GetRepeatBusinessStatisticsCardsQuery, IEnumerable<CardItemModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetRepeatBusinessStatisticsCardsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<CardItemModel>> Handle(GetRepeatBusinessStatisticsCardsQuery request, CancellationToken cancellationToken)
    {
        /*var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var ordersQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Orders.Order>().Where(o => o.SellerId == sellerId);
        var orders = await _repository.ToListAsync<QuickHire.Domain.Orders.Order>(ordersQueryable);

        var buyerGroups = orders.GroupBy(x => x.BuyerId).Select(x => x.Count()).ToList();

        var totalRevenue = orders.Sum(x => x.TotalPrice);
        var returningBuyers = buyerGroups.Count(x => x > 1);
        var avgRepeatOrders = buyerGroups.Any() ? buyerGroups.Average() : 0;

        return new List<CardItemModel>
    {
        new CardItemModel
        {
            Title = "Returning buyers",
            Value = returningBuyers.ToString()
        },
        new CardItemModel
        {
            Title = "Avg repeat orders",
            Value = avgRepeatOrders.ToString("F1")
        },
        new CardItemModel
        {
            Title = "Revenue",
            Value = totalRevenue.ToString("C")
        }
    };*/

        return new List<CardItemModel>
        {
            new CardItemModel
            {
                Title = "Returning buyers",
                Value = "230"
            },
            new CardItemModel
            {
                Title = "Average repeat orders",
                Value = "1.7"
            },
            new CardItemModel
            {
                Title = "Revenue",
                Value = "15,500"
            }
        };
    }
}

