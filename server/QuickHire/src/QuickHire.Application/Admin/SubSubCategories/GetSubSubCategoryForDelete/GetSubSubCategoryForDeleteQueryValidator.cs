using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.SubSubCategories.GetSubSubCategoryForDelete;

public class GetSubSubCategoryForDeleteQueryValidator : AbstractValidator<GetSubSubCategoryForDeleteQuery>
{
    public GetSubSubCategoryForDeleteQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

