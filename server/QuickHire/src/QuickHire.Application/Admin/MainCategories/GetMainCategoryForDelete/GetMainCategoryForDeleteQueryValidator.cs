using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.MainCategories.GetMainCategoryForDelete;

public class GetMainCategoryForDeleteQueryValidator : AbstractValidator<GetMainCategoryForDeleteQuery>
{
    public GetMainCategoryForDeleteQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"));           
    }
}

