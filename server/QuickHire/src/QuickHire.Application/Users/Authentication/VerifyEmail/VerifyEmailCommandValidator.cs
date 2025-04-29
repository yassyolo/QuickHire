using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Authentication.VerifyEmail;

internal class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(x => x.model.Token)
            .NotEmpty()
            .WithMessage(Required)
            .EmailAddress()
            .WithMessage(InvalidEmail);

        RuleFor(x => x.model.UserId)
            .NotEmpty()
            .WithMessage(Required);
    }
}
