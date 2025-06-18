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
        var orderQueryable = _repository.GetAllIncluding<Domain.Orders.Order>(x => x.Gig!, x => x.SelectedPaymentPlan!).Where(x => x.Status == (OrderStatus)request.OrderStatusId);
        var ordersTableModels = new List<OrdersTableModel>();

        if (request.Buyer.HasValue)
        {
            var buyerId = await _userService.GetBuyerIdByUserIdAsync();
            orderQueryable = orderQueryable.Where(x => x.BuyerId == buyerId);
            var orders = await _repository.ToListAsync(orderQueryable);

            foreach (var x in orders)
            {
                var sellerUserId = await _userService.GetUserIdBySellerIdAsync(x.SellerId);
                var sellerUserName = await _userService.GetUsernameByUserIdAsync(sellerUserId);
                ordersTableModels.Add(new OrdersTableModel
                {
                    Id = x.Id,
                    SellerUsername = sellerUserName,
                    GigTitle = x.Gig!.Title,
                    DueOn = x.CreatedAt.AddDays(x.SelectedPaymentPlan!.DeliveryTimeInDays).ToString(),
                    Total = x.TotalPrice,
                    Status = x.Status.ToString()
                });
            }
        }
        else
        {
            var sellerId = await _userService.GetSellerIdByUserIdAsync();
            orderQueryable = orderQueryable.Where(x => x.SellerId == sellerId && x.Id == x.Id);
            var orders = await _repository.ToListAsync(orderQueryable);
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

        }

        return ordersTableModels;
    }
}
