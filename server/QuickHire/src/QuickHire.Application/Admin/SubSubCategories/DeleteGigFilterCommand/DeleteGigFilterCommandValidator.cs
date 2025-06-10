using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ModerationItem;

namespace QuickHire.Application.Admin.SubSubCategories.DeleteGigFilterCommand;

public class DeleteGigFilterCommandValidator : AbstractValidator<DeleteGigFilterCommand>
{
    public DeleteGigFilterCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Reason"))
            .MaximumLength(ReasonMaxLength)
            .WithMessage(string.Format(StringLength, "Reason", 0, 500))
            .MinimumLength(ReasonMinLength)
            .WithMessage(string.Format(StringLength, "Reason", 0, 500));
    }
}

