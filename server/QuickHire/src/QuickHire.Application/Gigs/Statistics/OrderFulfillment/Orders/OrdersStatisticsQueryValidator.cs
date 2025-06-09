using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.Statistics.OrderFulfillment.Orders;

public class OrdersStatisticsQueryValidator : AbstractValidator<OrdersStatisticsQuery>
{
    public OrdersStatisticsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

