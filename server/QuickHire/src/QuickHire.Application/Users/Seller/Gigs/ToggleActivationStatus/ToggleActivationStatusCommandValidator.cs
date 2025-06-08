using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Seller.Gigs.ToggleActivationStatus;

public class ToggleActivationStatusCommandValidator : AbstractValidator<ToggleActivationStatusCommand>
{
    public ToggleActivationStatusCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .WithMessage(string.Format(Required, "Id"))
           .NotNull()
           .WithMessage(string.Format(Required, "Id"))
           .GreaterThan(0)
           .WithMessage(string.Format(GreaterThan, "Id", 0));

        RuleFor(x => x.Paused)
          .NotEmpty()
          .WithMessage(string.Format(Required, "Paused"));
    }
}

