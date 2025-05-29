using FluentValidation;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Category;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubCategories.AddSubCategory;

public class AddSubCategoryCommandValidator : AbstractValidator<AddSubCategoryCommand>
{
    public AddSubCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Name"))
            .MaximumLength(NameMaxLength)
            .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength));

        RuleFor(x => x.Image)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Image"));

        RuleFor(x => x.MainCategoryId)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Main Category Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Main Category Id", 0));
    }
}

