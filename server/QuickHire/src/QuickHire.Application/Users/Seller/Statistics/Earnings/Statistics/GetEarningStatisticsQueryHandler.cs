using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;
using QuickHire.Application.Users.Seller.Statistics.Earnings.Statistics;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Orders.Enums;

namespace QuickHire.Application.Users.Seller.Statistics.Earnings.Data;

public class GetEarningStatisticsQueryHandler : IQueryHandler<GetEarningStatisticsQuery, IEnumerable<EarningStatisticsRowModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetEarningStatisticsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<EarningStatisticsRowModel>> Handle(GetEarningStatisticsQuery request, CancellationToken cancellationToken)
    {
        /*var (startDate, endDate) = ParseRange(request.Range ?? "last 30 days");

        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var ordersQuery = _repository.GetAllReadOnly<Order>().Where(x => x.SellerId == sellerId && x.CreatedAt.Date >= startDate&& x.CreatedAt.Date <= endDate);
        var orders = await _repository.ToListAsync(ordersQuery);

        var dateRange = Enumerable.Range(0, (endDate - startDate).Days + 1)
            .Select(x => startDate.AddDays(x)).ToList();

        var result = dateRange.Select(date =>
        {
            var dateOnly = date.Date;

            var ordersOnDate = orders.Where(x => x.CreatedAt.Date == dateOnly).ToList();

            var totalRevenue = ordersOnDate.Sum(x => x.TotalPrice);
            var completedRevenue = ordersOnDate.Where(x => x.Status == OrderStatus.Delivered).Sum(x => x.TotalPrice);
            var inProgressRevenue = ordersOnDate.Where(x =>x.Status == OrderStatus.InProgress).Sum(x => x.TotalPrice);

            var orderCount = ordersOnDate.Count;
            var averageOrderValue = orderCount == 0 ? 0 : totalRevenue / orderCount;

            return new EarningStatisticsRowModel
            {
                Date = dateOnly.ToString("yyyy-MM-dd"),
                TotalRevenue = totalRevenue,
                CompletedRevenue = completedRevenue,
                InProgressRevenue = inProgressRevenue,
                AverageOrderValue = averageOrderValue
            };
        });

        return result;*/

        return new List<EarningStatisticsRowModel>
        {
            new EarningStatisticsRowModel
            {
                Date = "2023-10-01",
                TotalRevenue = 1000,
                CompletedRevenue = 800,
                InProgressRevenue = 200,
                AverageOrderValue = 200
            },
            new EarningStatisticsRowModel
            {
                Date = "2023-10-02",
                TotalRevenue = 1200,
                CompletedRevenue = 1000,
                InProgressRevenue = 200,
                AverageOrderValue = 300
            },
            new EarningStatisticsRowModel
            {
                Date = "2023-10-03",
                TotalRevenue = 1500,
                CompletedRevenue = 1200,
                InProgressRevenue = 300,
                AverageOrderValue = 375
            },
            new EarningStatisticsRowModel
            {
                Date = "2023-10-04",
                TotalRevenue = 900,
                CompletedRevenue = 700,
                InProgressRevenue = 200,
                AverageOrderValue = 225
            },
            new EarningStatisticsRowModel
            {
                Date = "2023-10-05",
                TotalRevenue = 1100,
                CompletedRevenue = 900,
                InProgressRevenue = 200,
                AverageOrderValue = 275
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
