using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.CustomOffers;

public class CustomOfferInclusives : BaseSoftDeletableEntity<int>
{
    public int CustomOfferId { get; set; }
    public CustomOffer CustomOffer { get; set; } = null!;
    public PaymentPlanInclude PaymentPlanInclude { get; set; } = null!;
    public int PaymentPlanIncludeId { get; set; }
}
