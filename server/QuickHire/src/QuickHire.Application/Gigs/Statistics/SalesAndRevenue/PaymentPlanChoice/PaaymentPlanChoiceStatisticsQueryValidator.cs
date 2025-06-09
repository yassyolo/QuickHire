using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.Statistics.SalesAndRevenue.PaymentPlanChoice;

public class PaaymentPlanChoiceStatisticsQueryValidator : AbstractValidator<PaaymentPlanChoiceStatisticsQuery>
{
    public PaaymentPlanChoiceStatisticsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}
