using FluentValidation;
using Newtonsoft.Json;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Gigs.GigsInSubSubCategory;

public class GigsInSubSubCategoryQueryValidator : AbstractValidator<GigsInSubSubCategoryQuery>
{
    public GigsInSubSubCategoryQueryValidator()
    {
        RuleFor(x => x.ItemsPerPage)
            .NotEmpty()
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.Required, "ItemsPerPage"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "ItemsPerPage", 0));


        RuleFor(x => x.CurrentPage)
            .NotEmpty()
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.Required, "Current page"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "CurrentPage", 0));       
    }
}

