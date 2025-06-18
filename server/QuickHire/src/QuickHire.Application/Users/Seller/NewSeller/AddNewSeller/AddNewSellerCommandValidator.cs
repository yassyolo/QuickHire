using FluentValidation;
using Newtonsoft.Json;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ApplicationUser;

namespace QuickHire.Application.Users.Seller.NewSeller.AddNewSeller;

public class AddNewSellerCommandValidator : AbstractValidator<AddNewSellerCommand>
{
    public AddNewSellerCommandValidator()
    {
        RuleFor(x => x.Certifications)
           .NotEmpty()
  .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.Required, "Certifications"));

       RuleFor(x => x.Educations)
            .NotEmpty()
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.Required, "Educations"));

        RuleFor(x => x.Skills)
            .NotEmpty()
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.Required, "Skills"));

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.Required, "Full Name"))
            .MaximumLength(FullNameMaxLength)
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.StringLength, "Full Name", FullNameMinLength, FullNameMaxLength))
            .MinimumLength(FullNameMinLength)
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.StringLength, "Full Name", FullNameMinLength, FullNameMaxLength));           

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.Required, "Description"))
            .MaximumLength(DescriptionMaxLength)
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.StringLength, "Description", DescriptionMinLength, DescriptionMaxLength))
            .MinimumLength(DescriptionMinLength)
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.StringLength, "Description", DescriptionMinLength, DescriptionMaxLength));
            

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.Required, "Username"))
            .MaximumLength(UsernameMaxLength)
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.StringLength, "Username", UsernameMinLength, UsernameMaxLength))
            .MinimumLength(UsernameMinLength)
            .WithMessage(string.Format(QuickHire.Application.Common.Constants.ValidationMessages.StringLength, "Username", UsernameMinLength, UsernameMaxLength));
    }
}
