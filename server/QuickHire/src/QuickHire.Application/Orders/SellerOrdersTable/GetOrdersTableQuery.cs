using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Orders;

namespace QuickHire.Application.Orders.GetOrdersTable;

public record GetOrdersTableQuery(int OrderStatusId) : IQuery<List<OrdersTableModel>>;

