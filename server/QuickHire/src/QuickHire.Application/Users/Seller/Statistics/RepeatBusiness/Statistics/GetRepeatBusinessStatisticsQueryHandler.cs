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

    /* const fetchStatistics = () => {
        const mockData: RepeatBusinessStatistics[] = [
            { date: "2024-05-01", returningBuyers: 10, averageRepeatOrders: 1.5, revenue: 1200 },
            { date: "2024-05-02", returningBuyers: 12, averageRepeatOrders: 1.6, revenue: 1300 },
            { date: "2024-05-03", returningBuyers: 15, averageRepeatOrders: 1.8, revenue: 1600 },
            { date: "2024-05-04", returningBuyers: 14, averageRepeatOrders: 1.7, revenue: 1500 },
            { date: "2024-05-05", returningBuyers: 20, averageRepeatOrders: 1.9, revenue: 2000 },
        ];
        setStatistics(mockData);
    };*/
    public async Task<IEnumerable<RepeatBusinessRowModel>> Handle(GetRepeatBusinessStatisticsQuery request, CancellationToken cancellationToken)
    {
        /*var sellerId = await _userService.GetSellerIdByUserIdAsync();

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

        return groupedByDate;*/

        return new List<RepeatBusinessRowModel>
        {
            new RepeatBusinessRowModel
            {
                Date = "2024-05-01",
                ReturningBuyers = 10,
                AverageRepeatOrders = 1.5,
                Revenue = 1200
            },
            new RepeatBusinessRowModel
            {
                Date = "2024-05-02",
                ReturningBuyers = 12,
                AverageRepeatOrders = 1.6,
                Revenue = 1300
            },
            new RepeatBusinessRowModel
            {
                Date = "2024-05-03",
                ReturningBuyers = 15,
                AverageRepeatOrders = 1.8,
                Revenue = 1600
            },
            new RepeatBusinessRowModel
            {
                Date = "2024-05-04",
                ReturningBuyers = 14,
                AverageRepeatOrders = 1.7,
                Revenue = 1500
            },
            new RepeatBusinessRowModel
            {
                Date = "2024-05-05",
                ReturningBuyers = 20,
                AverageRepeatOrders = 1.9,
                Revenue = 2000
            }
        };
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

