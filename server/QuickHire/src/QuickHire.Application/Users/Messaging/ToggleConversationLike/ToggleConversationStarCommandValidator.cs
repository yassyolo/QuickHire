using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Messaging.ToggleConversationLike;

public class ToggleConversationStarCommandValidator : AbstractValidator<ToggleConversationStarCommand>
{
    public ToggleConversationStarCommandValidator()
    {
        RuleFor(x => x.MessageId)
             .NotEmpty()
             .WithMessage(string.Format(Required, "Id"))
             .GreaterThan(0)
             .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}
