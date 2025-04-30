using FluentValidation;

namespace QuickHire.Application.Users.Authentication.RefreshToken;

internal class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.model.Token)
            .NotEmpty()
            .WithMessage("Token is required.");
    }
}

