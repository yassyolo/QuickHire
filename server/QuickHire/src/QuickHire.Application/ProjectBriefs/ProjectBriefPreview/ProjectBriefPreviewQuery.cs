using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.ProjectBriefs;

namespace QuickHire.Application.ProjectBriefs.ProjectBriefPreview;

public record ProjectBriefPreviewQuery(int Id) : IQuery<ProjectBriefPreviewModel>
;
