using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Messaging.GetMessagesForConversation;

public class GetMessagesForConversationQueryValidator : AbstractValidator<GetMessagesForConversationQuery>
{
    public GetMessagesForConversationQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}

