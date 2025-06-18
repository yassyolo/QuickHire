using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Orders.Models.Details;

namespace QuickHire.Application.Orders.OrderDetails;

public record GetOrderDetailsQuery(int Id) : IQuery<OrderDetailsModel>;
