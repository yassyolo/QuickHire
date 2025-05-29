using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubCategories.PopularSubcategories;

public class PopularSubCategoriesQueryValidator : AbstractValidator<PopularSubcategoriesQuery>
{
    public PopularSubCategoriesQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"));
    }
}

