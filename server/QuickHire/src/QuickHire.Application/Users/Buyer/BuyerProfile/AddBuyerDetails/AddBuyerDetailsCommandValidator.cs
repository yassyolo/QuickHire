using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ApplicationUser;

namespace QuickHire.Application.Users.Buyer.BuyerProfile.AddBuyerDetails;

public class AddBuyerDetailsCommandValidator : AbstractValidator<AddBuyerDetailsCommand>
{
    public AddBuyerDetailsCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Description"))
            .MaximumLength(DescriptionMaxLength)
            .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength));

        RuleFor(x => x.Image)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Image"));
    }
}

