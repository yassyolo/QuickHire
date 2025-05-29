using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Category;

namespace QuickHire.Application.Admin.SubSubCategories.EditSubSubCategory;

public class EditSubSubCategoryCommandValidator : AbstractValidator<EditSubSubCategoryCommand>
{
    public EditSubSubCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Name"))
            .MaximumLength(NameMaxLength)
            .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength));
    }
}

