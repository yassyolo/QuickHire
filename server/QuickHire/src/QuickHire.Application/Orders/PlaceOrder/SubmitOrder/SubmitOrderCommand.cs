using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Orders.Models.Form;
using System.Windows.Input;

namespace QuickHire.Application.Orders.PlaceOrder.SubmitOrder;

public record SubmitOrderCommand(int? GigId, int? CustomOfferId, int PaymentPlanId, int BillingDetailsId, AnsweredRequirementModel[] Requirements) : ICommand<PaymentIntentResponse>;

