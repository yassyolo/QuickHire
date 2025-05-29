using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Review;

namespace QuickHire.Application.Orders.Ratings.Reviews;

public class AddReviewCommandValidator : AbstractValidator<AddReviewCommand>
{
    public AddReviewCommandValidator()
    {
        RuleFor(x => x.Rating)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Rating"));

        RuleFor(x => x.Comment)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Comment"))
            .MaximumLength(CommentMaxLength)
            .WithMessage(string.Format(StringLength, "Comment", CommentMinLength, CommentMaxLength))
            .MinimumLength(CommentMinLength)
            .WithMessage(string.Format(StringLength, "Comment", CommentMinLength, CommentMaxLength));

        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage(string.Format(Required, "OrderId"));
    }

}
