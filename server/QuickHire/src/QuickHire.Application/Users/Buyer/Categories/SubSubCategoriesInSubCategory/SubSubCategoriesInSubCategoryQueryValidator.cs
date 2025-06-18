using FluentValidation;
using Newtonsoft.Json;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Buyer.Categories.SubSubCategoriesInSubCategory;

public class SubSubCategoriesInSubCategoryQueryValidator : AbstractValidator<SubSubCategoriesInSubCategoryQuery>
{
    public SubSubCategoriesInSubCategoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Common.Constants.ValidationMessages.Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}
