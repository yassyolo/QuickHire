using QuickHire.Domain.Categories;
using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.ProjectBriefs.Enums;
using QuickHire.Domain.Shared.Implementations;
using QuickHire.Domain.Users;

namespace QuickHire.Domain.ProjectBriefs;

public class ProjectBrief : BaseSoftDeletableEntity<int>
{
    public string ProjectBriefNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AboutBuyer { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public int DeliveryTimeInDays { get; set; } 
    public int SubSubCategoryId { get; set; }
    public SubSubCategory SubSubCategory { get; set; } = null!;
    public int BuyerId { get; set; } 
    public Buyer Buyer { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
    public DateTime? WithdrawnAt { get; set; }
    public ProjectBriefStatus Status { get; set; }
    public List<CustomOffer>? CustomOffers { get; set; } 

}
