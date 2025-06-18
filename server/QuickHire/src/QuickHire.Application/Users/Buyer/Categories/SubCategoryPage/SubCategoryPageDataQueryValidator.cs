using FluentValidation;
using Newtonsoft.Json;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Buyer.Categories.SubCategoryPage;

public class SubCategoryPageDataQueryValidator : AbstractValidator<SubCategoryPageDataQuery>
{
    public SubCategoryPageDataQueryValidator()
    {
        RuleFor(x => x.Id)
    .NotEmpty()
    .WithMessage(string.Format(Common.Constants.ValidationMessages.Required, "Id"));
    }
}
