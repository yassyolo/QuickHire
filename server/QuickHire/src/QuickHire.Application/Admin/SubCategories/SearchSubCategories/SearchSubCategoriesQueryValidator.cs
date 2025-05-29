using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubCategories.SearchSubCategories;

public class SearchSubCategoriesQueryValidator : AbstractValidator<SearchSubCategoriesQuery>
{
    public SearchSubCategoriesQueryValidator()
    {
        RuleFor(x => x.ItemsPerPage)
            .NotEmpty()
            .WithMessage(string.Format(Required, "ItemsPerPage"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "ItemsPerPage", 0));
            

        RuleFor(x => x.CurrentPage)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Current page"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "CurrentPage", 0));          
    }
}

