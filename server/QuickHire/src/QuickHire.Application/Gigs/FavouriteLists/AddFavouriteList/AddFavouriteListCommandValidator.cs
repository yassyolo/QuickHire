using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.FavouriteGigsList;


namespace QuickHire.Application.Gigs.FavouriteLists.AddFavouriteList;

public class AddFavouriteListCommandValidator : AbstractValidator<AddFavouriteListCommand>
{
    public AddFavouriteListCommandValidator()
    {
        RuleFor(x => x.Name)
                 .NotEmpty()
                 .WithMessage(string.Format(Required, "Name"))
                 .MaximumLength(NameMaxLength)
                 .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength))
                 .MinimumLength(NameMinLength)
                 .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength));

        RuleFor(x => x.Description)
            .MaximumLength(DescriptionMaxLength)
            .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength))
            .MinimumLength(DescriptionMinLength)
            .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength));
    }
}

