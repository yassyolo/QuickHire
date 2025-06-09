using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.BillingDetails;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Address;

namespace QuickHire.Application.Users.BillingsAndFinancialDocuments.EditBillingInfo;

public class EditBillingInfoCommandValidator : AbstractValidator<EditBillingInfoCommand>
{
    public EditBillingInfoCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Full Name"))
            .MaximumLength(FullNameMaxLength)
            .WithMessage(string.Format(StringLength, "Full Name", FullNameMinLength, FullNameMaxLength))
            .MinimumLength(FullNameMinLength)
            .WithMessage(string.Format(StringLength, "Full Name", FullNameMinLength, FullNameMaxLength));

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Company Name"))
            .MaximumLength(CompanyNameMaxLength)
            .WithMessage(string.Format(StringLength, "Company Name", CompanyNameMinLength, CompanyNameMaxLength))
            .MinimumLength(CompanyNameMinLength)
            .WithMessage(string.Format(StringLength, "Company Name", CompanyNameMinLength, CompanyNameMaxLength));

        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Street"))
            .MaximumLength(StreetMaxLength)
            .WithMessage(string.Format(StringLength, "Street", StreetMinLength, StreetMaxLength))
            .MinimumLength(StreetMinLength)
            .WithMessage(string.Format(StringLength, "Street", StreetMinLength, StreetMaxLength));

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage(string.Format(Required, "City"))
            .MaximumLength(CityMaxLength)
            .WithMessage(string.Format(StringLength, "City", CityMinLength, CityMaxLength))
            .MinimumLength(CityMinLength)
            .WithMessage(string.Format(StringLength, "City", CityMinLength, CityMaxLength));

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Zip Code"))
            .MaximumLength(ZipCodeMaxLength)
            .WithMessage(string.Format(StringLength, "Zip Code", ZipCodeMinLength, ZipCodeMaxLength))
            .MinimumLength(ZipCodeMinLength)
            .WithMessage(string.Format(StringLength, "Zip Code", ZipCodeMinLength, ZipCodeMaxLength));

        RuleFor(x => x.CountryId)
    .NotEmpty()
    .WithMessage(string.Format(Required, "Country Id"))
    .GreaterThan(0)
    .WithMessage(string.Format(GreaterThan, "Country Id", 0));
    }
}
