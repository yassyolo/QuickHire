using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ApplicationUser;

namespace QuickHire.Application.Users.Buyer.EditBuyerDetails;

public class EditBuyerCommandValidator : AbstractValidator<EditBuyerCommand>
{
    public EditBuyerCommandValidator()
    {
        RuleFor(x => x.Description)
    .NotEmpty()
    .WithMessage(string.Format(Required, "Description"))
    .MaximumLength(DescriptionMaxLength)
    .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength));
    }
}
