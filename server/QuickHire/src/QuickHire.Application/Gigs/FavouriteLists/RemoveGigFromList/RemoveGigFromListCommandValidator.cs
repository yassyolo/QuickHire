using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.FavouriteLists.RemoveGigFromList;

public class RemoveGigFromListCommandValidator : AbstractValidator<RemoveGigFromListCommand>
{
    public RemoveGigFromListCommandValidator()
    {
        RuleFor(x => x.FavouriteGigId)
           .NotEmpty()
           .WithMessage(string.Format(Required, "Id"))
           .GreaterThan(0)
           .WithMessage(string.Format(GreaterThan, "Id", 0));       
    }
}

