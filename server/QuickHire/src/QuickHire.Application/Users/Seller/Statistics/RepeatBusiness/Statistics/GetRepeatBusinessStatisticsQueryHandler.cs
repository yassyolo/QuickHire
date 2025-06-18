using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;
using QuickHire.Domain.Orders;

namespace QuickHire.Application.Users.Seller.Statistics.RepeatBusiness.Statistics;

public class GetRepeatBusinessStatisticsQueryHandler : IQueryHandler<GetRepeatBusinessStatisticsQuery, IEnumerable<RepeatBusinessRowModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetRepeatBusinessStatisticsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<RepeatBusinessRowModel>> Handle(GetRepeatBusinessStatisticsQuery request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var (startDate, endDate) = ParseRange(request.Range ?? "last 30 days");

        var ordersQueryable = _repository.GetAllReadOnly<Order>().Where(x => x.SellerId == sellerId && x.CreatedAt >= startDate && x.CreatedAt <= endDate);
        var orders = await _repository.ToListAsync(ordersQueryable);

        var groupedByDate = orders
            .GroupBy(x => x.CreatedAt.Date)
            .Select(group =>
            {
                var buyers = group.GroupBy(x => x.BuyerId).ToList();
                var returningBuyers = buyers.Count(x => x.Count() > 1);
                var avgRepeatOrders = buyers.Any() ? buyers.Average(b => b.Count()) : 0;
                var revenue = group.Sum(x => x.TotalPrice);

                return new RepeatBusinessRowModel
                {
                    Date = group.Key.ToString("yyyy-MM-dd"),
                    ReturningBuyers = returningBuyers,
                    AverageRepeatOrders = avgRepeatOrders,
                    Revenue = revenue
                };
            })
            .OrderBy(x => x.Date)
            .ToList();

        return groupedByDate;
    }

    private (DateTime Start, DateTime End) ParseRange(string range)
    {
        var today = DateTime.UtcNow.Date;

        return range.Trim().ToLower() switch
        {
            "last 30 days" => (today.AddDays(-29), today),
            "last 3 months" => (today.AddMonths(-3), today),
            "yearly" => (new DateTime(today.Year, 1, 1), today),
            _ => (today.AddDays(-29), today)
        };
    }
}

