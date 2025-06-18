using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.Seller.DeleteGig;

public class DeleteGigCommandValidator : AbstractValidator<DeleteGigCommand>
{
    public DeleteGigCommandValidator()
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

