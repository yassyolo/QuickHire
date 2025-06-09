using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.FilterOption;

namespace QuickHire.Application.Admin.SubSubCategories.EditFilterOption;

public class EditFilterOptionCommandValidator : AbstractValidator<EditFilterOptionCommand>
{
    public EditFilterOptionCommandValidator()
    {
        RuleFor(x => x.Id)
             .NotEmpty()
             .WithMessage(string.Format(Required, "Id"));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Name"))
            .MinimumLength(NameMaxLength)
            .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength))
            .MaximumLength(NameMinLength)
            .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength));
    }
}

