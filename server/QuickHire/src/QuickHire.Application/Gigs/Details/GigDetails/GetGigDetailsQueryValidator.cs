using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.Details.GigDetails;

public class GetGigDetailsQueryValidator : AbstractValidator<GetGigDetailsQuery>
{
    public GetGigDetailsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));

        RuleFor(x => x.Preview)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Preview"));
    }
}

