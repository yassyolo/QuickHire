using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Buyer.Categories.SubCategories;

public class PopularSubCategoriesQueryValidator : AbstractValidator<PopularSubcategoriesQuery>
{
    public PopularSubCategoriesQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"));
    }
}

