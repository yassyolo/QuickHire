using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ModerationItem;

namespace QuickHire.Application.Admin.Reporting.Report;

public class ReportItemCommandValidator : AbstractValidator<ReportItemCommand>
{
    public ReportItemCommandValidator()
    {
        RuleFor(x => x.Reason)
    .NotEmpty()
    .WithMessage(string.Format(Required, "Reason"))
    .MaximumLength(ReasonMaxLength)
    .WithMessage(string.Format(StringLength, "Reason", 0, 500))
    .MinimumLength(ReasonMinLength)
    .WithMessage(string.Format(StringLength, "Reason", 0, 500));
    }
}
