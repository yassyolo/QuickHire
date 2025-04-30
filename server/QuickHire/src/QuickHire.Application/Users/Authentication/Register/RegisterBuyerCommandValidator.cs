using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Authentication.Register
{
    internal class RegisterBuyerCommandValidator : AbstractValidator<RegisterBuyerCommand>
    {
        public RegisterBuyerCommandValidator()
        {
            RuleFor(x => x.model.Email)
                .NotEmpty()
                .WithMessage(Required)
                .EmailAddress()
                .WithMessage(InvalidEmail);

            RuleFor(x => x.model.Password)
                .NotEmpty()
                .WithMessage(Required)
                .Matches(@"[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[0-9]")
                .WithMessage("Password must contain at least one number.")
                .Matches(@"[a-z]")
                .WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[\W_]")
                .WithMessage("Password must contain at least one special character.")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.");
        }
    }   
}
