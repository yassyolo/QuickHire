using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Category;

namespace QuickHire.Application.Admin.SubSubCategories.AddSubSubCategory;

public class AddSubSubCategoryCommandValidator : AbstractValidator<AddSubSubCategoryCommand>
{
    public AddSubSubCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Name"))
            .MaximumLength(NameMaxLength)
            .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength));

        RuleFor(x => x.SubCategoryId)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Main Category Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Main Category Id", 0));
    }
}
