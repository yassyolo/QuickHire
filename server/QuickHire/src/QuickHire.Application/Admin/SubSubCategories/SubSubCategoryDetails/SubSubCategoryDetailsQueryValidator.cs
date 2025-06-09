using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubSubCategories.SubSubCategoryDetails;

public class SubSubCategoryDetailsQueryValidator : AbstractValidator<SubSubCategoryDetailsQuery>
{
    public SubSubCategoryDetailsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}
