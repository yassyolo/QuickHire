using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using Newtonsoft.Json;

namespace QuickHire.Application.Orders.PlaceOrder.SuccessfulPayment;

public class MarkOrderPaidCommandValidator : AbstractValidator<MarkOrderPaidCommand>
{
    public MarkOrderPaidCommandValidator()
    {
        RuleFor(x => x.OrderId)
    .NotEmpty()
    .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.Required, "OrderId"))
    .GreaterThan(0)
    .WithMessage(string.Format(GreaterThan, "OrderId", 0));
    }
}

