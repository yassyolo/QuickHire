using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.Notifications.MarkAsRead;

public class MarkAsReadQueryValidator : AbstractValidator<MarkAsReadQuery>
{
    public MarkAsReadQueryValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .WithMessage(string.Format(Required, "Id"));
    }
}

