using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Buyer.Categories.MainCategoryDetails.SubCategoriesInMainCategory;

public class SubCategoriesInMainCategoryQueryValidator : AbstractValidator<SubCategoriesInMainCategoryQuery>
{
    public SubCategoriesInMainCategoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Main Category Id"));
    }
}
