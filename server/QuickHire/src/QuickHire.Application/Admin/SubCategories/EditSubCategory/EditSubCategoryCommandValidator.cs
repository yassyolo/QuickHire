using FluentValidation;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Category;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubCategories.EditSubCategory;

public class EditSubCategoryCommandValidator : AbstractValidator<EditSubCategoryCommand>
{
    public EditSubCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage(string.Format(Required, "Name")) 
           .MaximumLength(NameMaxLength)
           .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength))
           .MinimumLength(NameMinLength)
           .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength));

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"));
    }
}

