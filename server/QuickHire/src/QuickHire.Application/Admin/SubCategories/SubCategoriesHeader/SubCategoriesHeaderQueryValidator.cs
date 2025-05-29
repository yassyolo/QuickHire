using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubCategories.SubCategoriesHeader;

public class SubCategoriesHeaderQueryValidator : AbstractValidator<SubCategoriesHeaderQuery>
{
    public SubCategoriesHeaderQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"));
    }
}
