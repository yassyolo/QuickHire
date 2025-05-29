using FluentValidation;

namespace QuickHire.Application.Users.Authentication.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Model.Token)
            .NotEmpty()
            .WithMessage("Token is required.");
    }
}

