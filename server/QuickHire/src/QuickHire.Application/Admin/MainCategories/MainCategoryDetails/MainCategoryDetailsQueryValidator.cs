using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
namespace QuickHire.Application.Admin.MainCategories.MainCategoryDetails;

public class MainCategoryDetailsQueryValidator : AbstractValidator<MainCategoryDetailsQuery>
{
    public MainCategoryDetailsQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

