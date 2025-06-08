using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Certification;

namespace QuickHire.Application.Users.Seller.Profile.EditCertification;

public class EditCertificationCommandValidator : AbstractValidator<EditCertificationCommand>
{
    public EditCertificationCommandValidator()
    {
        RuleFor(x => x.Certifications)
            .NotEmpty()
   .WithMessage(string.Format(Required, "Certifications"));

        RuleForEach(x => x.Certifications).ChildRules(cert =>
        {
           
            cert.RuleFor(x => x.Id)
   .NotEmpty()
   .WithMessage(string.Format(Required, "Id"));

            cert.RuleFor(x => x.Certification)
                .NotEmpty().WithMessage("Certification name is required.")
                .MaximumLength(NameMaxLength)
                .WithMessage(string.Format(StringLength, "Certification name", NameMinLength, NameMaxLength))
                .MinimumLength(NameMinLength)
                .WithMessage(string.Format(StringLength, "Certification name", NameMinLength, NameMaxLength));
            cert.RuleFor(x => x.Issuer)
                 .NotEmpty().WithMessage("Issuer is required.")
                 .MaximumLength(IssuerMaxLength)
                 .WithMessage(string.Format(StringLength, "Issuer", IssuerMinLength, IssuerMaxLength));

            cert.RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required.");
        });
    }
}

