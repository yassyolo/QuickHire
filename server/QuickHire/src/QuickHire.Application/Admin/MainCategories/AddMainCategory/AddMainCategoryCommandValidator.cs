using FluentValidation;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Category;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.MainCategory;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.FAQ;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Admin.MainCategories.AddMainCategory;

public class AddMainCategoryCommandValidator : AbstractValidator<AddMainCategoryCommand>
{
    public AddMainCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Name"))
            .MaximumLength(NameMaxLength)
            .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength))
            .MinimumLength(NameMinLength)
            .WithMessage(string.Format(StringLength, "Name", NameMinLength, NameMaxLength));

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Description"))
            .MaximumLength(DescriptionMaxLength)
            .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength))
            .MinimumLength(DescriptionMinLength)
            .WithMessage(string.Format(StringLength, "Description", DescriptionMinLength, DescriptionMaxLength));

        RuleForEach(x => x.Faqs).ChildRules(x =>
        {
            x.RuleFor(x => x.Question)
                .NotEmpty()
                .WithMessage(string.Format(Required, "Question"))
                .MaximumLength(QuestionMaxLength)
                .WithMessage(string.Format(StringLength, "Question", QuestionMinLength, QuestionMaxLength))
                .MinimumLength(QuestionMinLength)
                .WithMessage(string.Format(StringLength, "Question", QuestionMinLength, QuestionMaxLength));

            x.RuleFor(x => x.Answer)
            .NotEmpty()
                .WithMessage(string.Format(Required, "Answer"))
                .MaximumLength(AnswerMaxLength)
                .WithMessage(string.Format(StringLength, "Answer", AnswerMinLength, AnswerMaxLength))
                .MinimumLength(AnswerMinLength)
                .WithMessage(string.Format(StringLength, "Answer", AnswerMinLength, AnswerMaxLength));
        });

    }
}
