using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.Statistics.CustomerFeedback.RatingDistribution;

public class RatingDistributionStatisticsQueryValidator : AbstractValidator<RatingDistributionStatisticsQuery>
{
    public RatingDistributionStatisticsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}
