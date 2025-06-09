using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Orders;
using QuickHire.Domain.Orders.Enums;

namespace QuickHire.Application.Users.Seller.Orders.GetOrdersTable;

public class GetOrdersTableQueryHandler : IQueryHandler<GetOrdersTableQuery, List<OrdersTableModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetOrdersTableQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<List<OrdersTableModel>> Handle(GetOrdersTableQuery request, CancellationToken cancellationToken)
    {
        /*var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var ordersQueryable = _repository.GetAllReadOnly<Domain.Orders.Order>().Where(x => x.SellerId == sellerId && x.Status == (OrderStatus)request.OrderStatusId);

        ordersQueryable = _repository.GetAllIncluding<Domain.Orders.Order>(x => x.Gig);
        var orders = await _repository.ToListAsync(ordersQueryable);

        var ordersTableModels = await Task.WhenAll(
    orders.Select(async x => new OrdersTableModel
    {
        Id = x.Id,
        BuyerUsername = await _userService.GetUsernameByUserIdAsync(x.BuyerId),
        GigTitle = x.Gig.Title,
        DueOn = x.CreatedAt.AddDays(x.SelectedPaymentPlan.DeliveryTimeInDays).ToString(),
        Total = x.TotalPrice,
        Status = x.Status.ToString()
    })
);

        return ordersTableModels.ToList();*/

        return new List<OrdersTableModel>
        {
            new OrdersTableModel
            {
                Id = 1,
                BuyerUsername = "buyer1",
                GigTitle = "Gig Title 1",
                DueOn = DateTime.Now.AddDays(3).ToString("dd MMM yyyy"),
                Total = 100.00m,
                Status = OrderStatus.Delivered.ToString()
            },
            new OrdersTableModel
            {
                Id = 2,
                BuyerUsername = "buyer2",
                GigTitle = "Gig Title 2",
                DueOn = DateTime.Now.AddDays(5).ToString("dd MMM yyyy"),
                Total = 150.00m,
                Status = OrderStatus.InProgress.ToString()
            }
        };
    }
}
