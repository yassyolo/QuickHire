using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.FAQ;

namespace QuickHire.Application.Shared.FAQ.EditFAQ;

public class EDitFAQCommandValidator : AbstractValidator<EditFAQCommand>
{
    public EDitFAQCommandValidator()
    {
        RuleFor(x => x.Question)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Question"))
            .MaximumLength(QuestionMaxLength)
            .WithMessage(string.Format(StringLength, "Question", QuestionMinLength, QuestionMaxLength))
            .MinimumLength(QuestionMinLength)
            .WithMessage(string.Format(StringLength, "Question", QuestionMinLength, QuestionMaxLength));

        RuleFor(x => x.Answer)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Answer"))
            .MaximumLength(AnswerMaxLength)
            .WithMessage(string.Format(StringLength, "Answer", AnswerMinLength, AnswerMaxLength))
            .MinimumLength(AnswerMinLength)
            .WithMessage(string.Format(StringLength, "Answer", AnswerMinLength, AnswerMaxLength));

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

