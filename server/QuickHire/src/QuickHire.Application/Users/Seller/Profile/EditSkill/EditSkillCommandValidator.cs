using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Seller.Profile.EditSkill;

public class EditSkillCommandValidator : AbstractValidator<EditSkillCommand>
{
    public EditSkillCommandValidator()
    {
        RuleForEach(x => x.Skills).ChildRules(x =>
        {

            x.RuleFor(x => x.Id)
   .NotEmpty()
   .WithMessage(string.Format(Required, "Id"));
        }
        );
    }
}

