using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Buyer.Categories.Header.SubCategoriesHeader;

public class SubCategoriesHeaderQueryValidator : AbstractValidator<SubCategoriesHeaderQuery>
{
    public SubCategoriesHeaderQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"));
    }
}
