using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubCategories.SubCategoriesInMainCategory;

public class SubCategoriesInMainCategoryQueryValidator : AbstractValidator<SubCategoriesInMainCategoryQuery>
{
    public SubCategoriesInMainCategoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Main Category Id"));
    }
}
