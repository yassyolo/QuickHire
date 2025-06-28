using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Dashboard;

namespace QuickHire.Application.Users.Seller.Dashboard.GetSellerDashboardOrders;

public class GetSellerDashboardOrdersQueryHandler : IQueryHandler<GetSellerDashboardOrdersQuery, IEnumerable<SellerDashboardOrderModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetSellerDashboardOrdersQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<SellerDashboardOrderModel>> Handle(GetSellerDashboardOrdersQuery request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var ordersQueryable = _repository.GetAllIncluding<QuickHire.Domain.Orders.Order>(x => x.Gig, x => x.Buyer, x => x.SelectedPaymentPlan).Where(x => x.SellerId == sellerId && (request.Active ? x.Status == QuickHire.Domain.Orders.Enums.OrderStatus.InProgress: x.Status == QuickHire.Domain.Orders.Enums.OrderStatus.Delivered));

        var ordersList = await _repository.ToListAsync<QuickHire.Domain.Orders.Order>(ordersQueryable);
        ordersList = ordersList.OrderByDescending(x => x.CreatedAt);

        return ordersList.Adapt<IEnumerable<SellerDashboardOrderModel>>();
    }
}
