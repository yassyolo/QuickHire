using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.Users;

namespace QuickHire.Domain.ProjectBriefs;

public class SuitableSellerProjectBrief 
{
    public int SellerId { get; set; }
    public Seller Seller { get; set; } = null!;
    public int ProjectBriefId { get; set; }
    public ProjectBrief ProjectBrief { get; set; } = null!;
    public int? CustomOfferId { get; set; }
    public CustomOffer? CustomOffer { get; set; } 
    public DateTime CreatedAt { get; set; } 
}
