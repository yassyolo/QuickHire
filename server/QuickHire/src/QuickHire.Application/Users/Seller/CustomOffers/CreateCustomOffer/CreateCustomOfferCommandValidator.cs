using FluentValidation;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.CustomOffer;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Seller.CustomOffers.CreateCustomOffer;

public class CreateCustomOfferCommandValidator : AbstractValidator<CreateCustomOfferCommand>
{
    public CreateCustomOfferCommandValidator()
    {
        RuleFor(x => x.Description)
    .NotEmpty()
    .WithMessage(string.Format(Required, "Description"))
    .MaximumLength(DescriptionMaxLength)
    .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength))
    .MinimumLength(DescriptionMinLength)
    .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength));

        RuleFor(x => x.ProjectBriefId)
            .NotEmpty()
            .WithMessage(string.Format(Required, "ProjectBriefId"));

        RuleFor(x => x.GigId)
            .NotEmpty()
            .WithMessage(string.Format(Required, "GigId"));

        RuleFor(x => x.DeliveryTime)
            .NotEmpty()
            .WithMessage(string.Format(Required, "DeliveryTime"))
            .GreaterThan(0)
            .WithMessage("Delivery time must be greater than zero.");

        RuleFor(x => x.Total)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Total"))
            .GreaterThan(0)
            .WithMessage("Total must be greater than zero.");

        RuleFor(x => x.InclusivesIds)
            .NotEmpty()
            .WithMessage(string.Format(Required, "InclusivesIds"))
            .Must(ids => ids.Length > 0)
            .WithMessage("At least one inclusive must be selected.");
    }
}
