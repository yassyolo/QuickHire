using QuickHire.Domain.CustomOffers;

namespace QuickHire.Domain.ProjectBriefs;

public class SuitableSellerProjectBrief 
{
    public string SellerId { get; set; } = string.Empty;
    public int ProjectBriefId { get; set; }
    public ProjectBrief ProjectBrief { get; set; } = null!;
    public int? CustomOfferId { get; set; }
    public CustomOffer? CustomOffer { get; set; } 
}
