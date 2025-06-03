using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.FavouriteLists.SaveGigToOldList;

public class SaveGigToOldListCommandValidator : AbstractValidator<SaveGigToOldListCommand>
{
    public SaveGigToOldListCommandValidator()
    {
        RuleFor(x => x.FavouriteListId)
           .NotEmpty()
           .WithMessage(string.Format(Required, "Id"))
           .GreaterThan(0)
           .WithMessage(string.Format(GreaterThan, "Id", 0));

        RuleFor(x => x.GigId)
   .NotEmpty()
   .WithMessage(string.Format(Required, "Id"))
   .GreaterThan(0)
   .WithMessage(string.Format(GreaterThan, "Id", 0));

    }
}

