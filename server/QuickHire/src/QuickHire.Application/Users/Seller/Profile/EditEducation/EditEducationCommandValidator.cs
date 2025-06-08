using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Education;


namespace QuickHire.Application.Users.Seller.Profile.EditEducation;

public class EditEducationCommandValidator : AbstractValidator<EditEducationCommand>
{
    public EditEducationCommandValidator()
    {
        RuleForEach(x => x.Educations).ChildRules(x =>
        {

            x.RuleFor(x => x.Id)
   .NotEmpty()
   .WithMessage(string.Format(Required, "Id"));

            x.RuleFor(x => x.EndYear)
                .NotEmpty().WithMessage(string.Format(Required, "End Year"));

            x.RuleFor(x => x.Major)
                .NotEmpty().WithMessage("Major is required.")
                .MaximumLength(MajorMaxLength)
                .WithMessage(string.Format(StringLength, "Major", MajorMinLength, MajorMaxLength))
                .MinimumLength(MajorMinLength)
                .WithMessage(string.Format(StringLength, "Major", MajorMinLength, MajorMaxLength));

            x.RuleFor(x => x.Institution)
                .NotEmpty().WithMessage("Institution name is required.")
                .MaximumLength(InstitutionMaxLength)
                .WithMessage(string.Format(StringLength, "Institution name", InstitutionMinLength, InstitutionMaxLength))
                .MinimumLength(InstitutionMinLength)
                .WithMessage(string.Format(StringLength, "Institution name", InstitutionMinLength, InstitutionMaxLength));

            x.RuleFor(x => x.Degree)
            .NotEmpty().WithMessage("Degree is required.");
        
        });
     }
}

