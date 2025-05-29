namespace QuickHire.Application.Admin.FAQ.AddFAQ;

using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.FAQ;

public class AddFAQCommandValidator : AbstractValidator<AddFAQCommand>
{
    public AddFAQCommandValidator()
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
    }
}

