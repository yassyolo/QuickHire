using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Buyer.Categories.MainCategoryDetails.MainCategoryPageDetails;

public class MainCategoryPageDeatilsQueryValidator : AbstractValidator<MainCategoryPageDeatilsQuery>
{
    public MainCategoryPageDeatilsQueryValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .WithMessage(string.Format(Required, "Id"));
    }
}

