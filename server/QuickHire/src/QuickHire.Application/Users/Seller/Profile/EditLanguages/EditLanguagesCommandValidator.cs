using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Seller.Profile.EditLanguages;

public class EditLanguagesCommandValidator : AbstractValidator<EditLanguagesCommand>
{
    public EditLanguagesCommandValidator()
    {
        RuleForEach(x => x.Languages).ChildRules(x =>
        {

            x.RuleFor(x => x.LanguageId)
   .NotEmpty()
   .WithMessage(string.Format(Required, "Id"));
        }
        );
}

}
