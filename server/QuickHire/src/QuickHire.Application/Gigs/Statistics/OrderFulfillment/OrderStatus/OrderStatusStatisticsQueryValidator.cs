using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.Statistics.OrderFulfillment.OrderStatus;

public class OrderStatusStatisticsQueryValidator : AbstractValidator<OrderStatusStatisticsQuery>
{
    public OrderStatusStatisticsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}
