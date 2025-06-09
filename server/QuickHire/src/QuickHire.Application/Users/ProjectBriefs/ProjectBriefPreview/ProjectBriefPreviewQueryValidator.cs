using FluentValidation;
using static QuickHire.Application.Common.Constants.ValidationMessages;

namespace QuickHire.Application.Users.ProjectBriefs.ProjectBriefPreview;

public class ProjectBriefPreviewQueryValidator : AbstractValidator<ProjectBriefPreviewQuery>
{
    public ProjectBriefPreviewQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(string.Format(Required, "Id"))
            .GreaterThan(0)
            .WithMessage(string.Format(GreaterThan, "Id", 0));
    }
}
