using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;
using QuickHire.Application.Users.Seller.Statistics.OrderFullfillment.Statistics;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Orders.Enums;

namespace QuickHire.Application.Users.Seller.Statistics.OrderFullfillment.Data;

public class GetOrderFullfillmentStatisticsQueryHandler
    : IQueryHandler<GetOrderFullfillmentStatisticsQuery, IEnumerable<OrderFullfillmentRowModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetOrderFullfillmentStatisticsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<OrderFullfillmentRowModel>> Handle(GetOrderFullfillmentStatisticsQuery request,CancellationToken cancellationToken)
    {
        /*var (startDate, endDate) = ParseRange(request.Range ?? "last 30 days");
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var ordersQueryable = _repository.GetAllIncluding<Order>(x => x.Reviews) .Where(x => x.SellerId == sellerId && x.CreatedAt.Date >= startDate && x.CreatedAt.Date <= endDate);

        var orders = await _repository.ToListAsync(ordersQueryable);

        var groupedByDate = orders
            .GroupBy(o => o.CreatedAt.Date)
            .OrderBy(g => g.Key)
            .Select(group =>
            {
                var completedOrders = group.Count(x => x.Status == OrderStatus.Delivered);
                var allReviews = group.SelectMany(x => x.Reviews);
                var averageRating = allReviews.Any() ? allReviews.Average(x => x.Rating) : 0;

                return new OrderFullfillmentRowModel
                {
                    Date = group.Key.ToString("yyyy-MM-dd"),
                    NewOrders = group.Count(),
                    CompletedOrders = completedOrders,
                    Sales = group.Count(), 
                    AverageRating = averageRating,
                    Revenue = group.Sum(x => x.TotalPrice)
                };
            });

        return groupedByDate.ToList();*/

        return new List<OrderFullfillmentRowModel>
        {
            new OrderFullfillmentRowModel
            {
                Date = "2023-10-01",
                NewOrders = 5,
                CompletedOrders = 3,
                Sales = 5,
                AverageRating = 4.5,
                Revenue = 1500.00m
            },
            new OrderFullfillmentRowModel
            {
                Date = "2023-10-02",
                NewOrders = 8,
                CompletedOrders = 6,
                Sales = 8,
                AverageRating = 4.0,
                Revenue = 2400.00m
            },
            new OrderFullfillmentRowModel
            {
                Date = "2023-10-03",
                NewOrders = 10,
                CompletedOrders = 7,
                Sales = 10,
                AverageRating = 4.2,
                Revenue = 3000.00m
            },
            new OrderFullfillmentRowModel
            {
                Date = "2023-10-04",
                NewOrders = 6,
                CompletedOrders = 4,
                Sales = 6,
                AverageRating = 4.8,
                Revenue = 1800.00m
            },
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
