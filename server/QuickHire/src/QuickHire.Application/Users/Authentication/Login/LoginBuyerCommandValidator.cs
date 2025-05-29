using FluentValidation;

namespace QuickHire.Application.Users.Authentication.Login;

public class LoginBuyerCommandValidator : AbstractValidator<LoginBuyerCommand>
{
    public LoginBuyerCommandValidator()
    {
        RuleFor(x => x.model.EmailOrUsername)
            .NotEmpty()
            .WithMessage("Email or Username is required.");

        RuleFor(x => x.model.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}

