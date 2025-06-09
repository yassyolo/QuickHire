using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.ProjectBriefs;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.ProjectBriefs.ProjectBriefPreview;

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
        /*var projectBrief = await _repository.GetByIdAsync<ProjectBrief, int>(request.Id);
        if (projectBrief == null)
        {
            throw new NotFoundException(nameof(Domain.ProjectBriefs.ProjectBrief), request.Id);
        }

        var buyerInfo = await _userService.GetBuyerInfoForProjectBriefAsync(projectBrief.BuyerId);
        return new ProjectBriefPreviewModel
        {
            ProjectBriefNumber = projectBrief.ProjectBriefNumber,
            Description = projectBrief.Description,
            AboutBuyer = projectBrief.AboutBuyer,
            Budget = projectBrief.Budget,
            DeliveryTimeInDays = projectBrief.DeliveryTimeInDays,
            SubSubCategoryName = projectBrief.SubSubCategory.Name,
            BuyerName = buyerInfo.BuyerName,
            BuyerProfilePictureUrl = buyerInfo.BuyerProfilePictureUrl,
            CreatedAt = projectBrief.CreatedAt.ToString("yyyy-MM-dd"),
            MemberSince = buyerInfo.MemberSince,
            Status = projectBrief.Status.ToString(),
            Location = buyerInfo.Location,
            Languages = buyerInfo.Languages
        };*/

        return new ProjectBriefPreviewModel
        {
            ProjectBriefNumber = "PB-12345",
            Description = "This is a sample project brief description.",
            AboutBuyer = "About the buyer.",
            Budget = 500,
            DeliveryTimeInDays = 7,
            SubSubCategoryName = "Web Development",
            BuyerName = "John Doe",
            BuyerProfilePictureUrl = "https://example.com/profile.jpg",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
            MemberSince = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"),
            Status = "Open",
            Location = "USA",
            Languages = new string[] { "test", "test2", "test3" }
        };

    }
}

