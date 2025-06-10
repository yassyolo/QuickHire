using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ModerationItem;

namespace QuickHire.Application.Admin.Users.DeactivateUser;

public class DeactivateUserCommandValidator : AbstractValidator<DeactivateUserCommand>
{
    public DeactivateUserCommandValidator()
    {
        RuleFor(x => x.Id)
        .NotEmpty()
        .WithMessage(string.Format(Required, "UserId"));

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Reason"))
            .MaximumLength(ReasonMaxLength)
            .WithMessage(string.Format(StringLength, "Reason", 0, 500))
            .MinimumLength(ReasonMinLength)
            .WithMessage(string.Format(StringLength, "Reason", 0, 500));
    }
}

