using FluentValidation;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Category;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.MainCategory;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.MainCategories.EditMainCategory;

public class EditMainCategoryCommandValidator : AbstractValidator<EditMainCategoryCommand>
{
    public EditMainCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Name"))
            .MaximumLength(NameMaxLength)
            .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength))
            .MinimumLength(NameMinLength)
            .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength));

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Description"))
            .MaximumLength(DescriptionMaxLength)
            .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength))
            .MinimumLength(DescriptionMinLength)
            .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength));

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"));
    }
}

