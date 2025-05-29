using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
namespace QuickHire.Application.Gigs.Statistics.SalesAndRevenue.Revenue;

public class RevenueStatisticsQueryValidator : AbstractValidator<RevenueStatisticsQuery>
{
    public RevenueStatisticsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

