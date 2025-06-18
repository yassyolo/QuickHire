using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Orders;
using QuickHire.Domain.Orders.Enums;

namespace QuickHire.Application.Orders.GetOrdersTable;

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
        var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var ordersQueryable = _repository.GetAllIncluding<Domain.Orders.Order>(x => x.Gig!, x => x.SelectedPaymentPlan!).Where(x => x.SellerId == sellerId && x.Status == (OrderStatus)request.OrderStatusId);
        var orders = await _repository.ToListAsync(ordersQueryable);

        var ordersTableModels = new List<OrdersTableModel>();

        foreach (var x in orders)
        {
            var buyerUserId = await _userService.GetUserIdByBuyerIdAsync(x.BuyerId);
            var buyerUsername = await _userService.GetUsernameByUserIdAsync(buyerUserId);

            ordersTableModels.Add(new OrdersTableModel
            {
                Id = x.Id,
                BuyerUsername = buyerUsername,
                GigTitle = x.Gig!.Title,
                DueOn = x.CreatedAt.AddDays(x.SelectedPaymentPlan!.DeliveryTimeInDays).ToString(),
                Total = x.TotalPrice,
                Status = x.Status.ToString()
            });
        }

        return ordersTableModels;
    }
}
