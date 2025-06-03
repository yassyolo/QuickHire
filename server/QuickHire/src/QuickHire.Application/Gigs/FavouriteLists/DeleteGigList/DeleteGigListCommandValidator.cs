using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.FavouriteLists.DeleteGigList;

public class DeleteGigListCommandValidator : AbstractValidator<DeleteGigListCommand>
{
    public DeleteGigListCommandValidator()
    {
        RuleFor(x => x.Id)
   .NotEmpty()
   .WithMessage(string.Format(Required, "Id"))
   .GreaterThan(0)
   .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

