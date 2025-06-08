using FluentValidation;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ApplicationUser;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Seller.Profile.EditDescription;

public class EditDescriptionCommandValidator : AbstractValidator<EditDescriptionCommand>
{
    public EditDescriptionCommandValidator()
    {
        RuleFor(x => x.Description)
    .MaximumLength(DescriptionMaxLength)
    .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength))
    .MinimumLength(DescriptionMinLength)
    .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength));
    }
}

