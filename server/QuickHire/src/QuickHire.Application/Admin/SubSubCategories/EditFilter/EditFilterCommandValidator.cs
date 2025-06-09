using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.GigFilter;

namespace QuickHire.Application.Admin.SubSubCategories.EditFilter;

public class EditFilterCommandValidator : AbstractValidator<EditFilterCommand>
{
    public EditFilterCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Name"))
            .MinimumLength(GigFilterTitleMinLength)
            .WithMessage(string.Format(StringLength, "Name", GigFilterTitleMinLength, GigFilterTitleMaxLength))
            .MaximumLength(GigFilterTitleMaxLength)
            .WithMessage(string.Format(StringLength, "Name", GigFilterTitleMinLength, GigFilterTitleMaxLength));
    }
}
