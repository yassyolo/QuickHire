namespace QuickHire.Application.Users.Models.ProjectBriefs;

public class ProjectBriefPreviewModel
{
    public string ProjectBriefNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AboutBuyer { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public int DeliveryTimeInDays { get; set; }
    public string SubSubCategoryName { get; set; } = string.Empty;
    public string BuyerName { get; set; } = string.Empty;
    public string BuyerProfilePictureUrl { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string MemberSince { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string[] Languages { get; set; } = Array.Empty<string>();
}
