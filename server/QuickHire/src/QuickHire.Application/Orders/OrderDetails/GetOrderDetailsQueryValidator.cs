using FluentValidation;

namespace QuickHire.Application.Orders.OrderDetails;

public class GetOrderDetailsQueryValidator : AbstractValidator<GetOrderDetailsQuery>
{
    public GetOrderDetailsQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Order ID must be greater than zero.");
    }
}

