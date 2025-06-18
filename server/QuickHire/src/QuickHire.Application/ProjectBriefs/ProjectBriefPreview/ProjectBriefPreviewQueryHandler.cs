using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.ProjectBriefs;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.ProjectBriefs.ProjectBriefPreview;

public class ProjectBriefPreviewQueryHandler : IQueryHandler<ProjectBriefPreviewQuery, ProjectBriefPreviewModel>
{
    public string[] Languages { get; set; } = Array.Empty<string>();

    private readonly IUserService _userService;
    private readonly IRepository _repository;

    public ProjectBriefPreviewQueryHandler(IUserService userService, IRepository repository)
    {
        _userService = userService;
        _repository = repository;
    }
    public async Task<ProjectBriefPreviewModel> Handle(ProjectBriefPreviewQuery request, CancellationToken cancellationToken)
    {
        var projectBriefQueryable = _repository.GetAllIncluding<ProjectBrief>(x => x.SubSubCategory).Where(x => x.Id == request.Id);
        var projectBrief = await _repository.FirstOrDefaultAsync(projectBriefQueryable);
        if (projectBrief == null)
        {
            throw new NotFoundException(nameof(ProjectBrief), request.Id);
        }

        var userId = await _userService.GetUserIdByBuyerIdAsync(projectBrief.BuyerId);

        var buyerInfo = await _userService.GetUserInfoForPreviewAsync(userId);
        return new ProjectBriefPreviewModel
        {
            ProjectBriefNumber = projectBrief.ProjectBriefNumber,
            Description = projectBrief.Description,
            AboutBuyer = projectBrief.AboutBuyer,
            Budget = projectBrief.Budget,
            DeliveryTimeInDays = projectBrief.DeliveryTimeInDays,
            SubSubCategoryName = projectBrief.SubSubCategory.Name,
            BuyerName = buyerInfo.Name,
            BuyerProfilePictureUrl = buyerInfo.ProfilePictureUrl,
            CreatedAt = projectBrief.CreatedAt.ToString("dd MMM, yyyy"),
            MemberSince = buyerInfo.MemberSince,
            Status = projectBrief.Status.ToString(),
            Location = buyerInfo.Location,
            Languages = buyerInfo.Languages
        };
    }
}

