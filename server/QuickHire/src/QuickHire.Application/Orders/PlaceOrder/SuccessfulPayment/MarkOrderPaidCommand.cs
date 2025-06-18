using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Orders.PlaceOrder.SuccessfulPayment;

public record MarkOrderPaidCommand(int OrderId) : ICommand<Unit>;

