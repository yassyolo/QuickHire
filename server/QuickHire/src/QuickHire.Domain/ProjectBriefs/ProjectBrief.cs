using QuickHire.Domain.Categories;
using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.ProjectBriefs.Enums;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.ProjectBriefs;

public class ProjectBrief : BaseSoftDeletableEntity<int>
{
    public string ProjectBriefNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AboutBuyer { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public int DeliveryTimeInDays { get; set; } 
    public int MainCategoryId { get; set; }
    public MainCategory MainCategory { get; set; } = null!;
    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; } = null!;
    public string BuyerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } 
    public DateTime? WithdrawnAt { get; set; }
    public ProjectBriefStatus Status { get; set; }
    public List<CustomOffer>? CustomOffers { get; set; } 
}
