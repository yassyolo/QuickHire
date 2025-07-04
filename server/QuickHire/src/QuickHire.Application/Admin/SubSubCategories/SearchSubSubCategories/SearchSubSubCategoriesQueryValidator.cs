﻿using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubSubCategories.SearchSubSubCategories;

public class SearchSubSubCategoriesQueryValidator : AbstractValidator<SearchSubSubCategoriesQuery>
{
    public SearchSubSubCategoriesQueryValidator()
    {
        RuleFor(x => x.CurrentPage)
        .NotEmpty()
        .WithMessage(string.Format(Required, "Current page"))
        .GreaterThan(0)
        .WithMessage(string.Format(GreaterThan, "CurrentPage", 0));

        RuleFor(x => x.ItemsPerPage)
            .NotEmpty()
            .WithMessage(string.Format(Required, "ItemsPerPage"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "ItemsPerPage", 0));
    }
}
