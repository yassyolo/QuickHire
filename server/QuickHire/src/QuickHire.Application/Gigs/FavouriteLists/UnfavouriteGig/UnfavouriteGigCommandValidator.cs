using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.FavouriteLists.UnfavouriteGig;

public class UnfavouriteGigCommandValidator : AbstractValidator<UnfavouriteGigCommand>
{
    public UnfavouriteGigCommandValidator()
    {
        RuleFor(x => x.Id)
.NotEmpty()
.WithMessage(string.Format(Required, "Id"))
.GreaterThan(0)
.WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

