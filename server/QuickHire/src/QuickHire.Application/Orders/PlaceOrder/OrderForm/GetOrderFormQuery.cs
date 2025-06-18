using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Orders.Models.Form;

namespace QuickHire.Application.Orders.PlaceOrder.OrderForm;

public record GetOrderFormQuery(int GigId) : IQuery<OrderFormModel>;

