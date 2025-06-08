using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Dashboard;

namespace QuickHire.Application.Users.Seller.Dashboard.GetSellerDashboardOrders;

public record GetSellerDashboardOrdersQuery(bool Active) : IQuery<IEnumerable<SellerDashboardOrderModel>>;

