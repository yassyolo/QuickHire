using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubCategories.GetSubCategoryForDelete;

internal class GetSubCategoryForDeleteQueryValidator : AbstractValidator<GetSubCategoryForDeleteQuery>
{
    public GetSubCategoryForDeleteQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

