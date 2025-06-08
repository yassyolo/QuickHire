using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Seller.Gigs.GetGigForDelete;

public class GetGigForDeleteQueryValidator : AbstractValidator<GetGigForDeleteQuery>
{
    public GetGigForDeleteQueryValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .WithMessage(string.Format(Required, "Id"))
           .NotNull()
           .WithMessage(string.Format(Required, "Id"))
           .GreaterThan(0)
           .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

