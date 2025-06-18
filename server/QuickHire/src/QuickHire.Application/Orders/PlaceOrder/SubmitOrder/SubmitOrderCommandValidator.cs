using FluentValidation;

namespace QuickHire.Application.Orders.PlaceOrder.SubmitOrder;

public class SubmitOrderCommandValidator : AbstractValidator<SubmitOrderCommand>
{
    public SubmitOrderCommandValidator()
    {
        RuleFor(x => x.PaymentPlanId)
            .NotEmpty()
            .WithMessage(string.Format(Common.Constants.ValidationMessages.Required, "PaymentPlanId"));

        RuleFor(x => x.Requirements)
            .NotEmpty()
            .WithMessage(string.Format(Common.Constants.ValidationMessages.Required, "Requirements"));

        RuleFor(x => x.BillingDetailsId).NotEmpty()
            .WithMessage(string.Format(Common.Constants.ValidationMessages.Required, "BillingDetailsId"));
    }
}
