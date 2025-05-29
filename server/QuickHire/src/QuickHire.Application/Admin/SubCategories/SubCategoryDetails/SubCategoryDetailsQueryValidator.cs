using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubCategories.SubCategoryDetails;

public class SubCategoryDetailsQueryValidator : AbstractValidator<SubCategoryDetailsQuery>
{
    public SubCategoryDetailsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));          
    }
}

