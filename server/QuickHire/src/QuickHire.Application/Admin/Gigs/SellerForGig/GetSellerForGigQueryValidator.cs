using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.Gigs.SellerForGig;

public class GetSellerForGigQueryValidator : AbstractValidator<GetSellerForGigQuery>
{
    public GetSellerForGigQueryValidator()
    {
        RuleFor(x => x.Id)
             .NotEmpty()
             .WithMessage(string.Format(Required, "Id"))
             .GreaterThan(0)
             .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}
