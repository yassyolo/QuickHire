using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Gigs;

public class PaymentPlan : BaseSoftDeletableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int GigId { get; set; }
    public Gig Gig { get; set; } = null!;
    public int DeliveryTimeInDays { get; set; }
    public int Revisions { get; set; }
    public string Description { get; set; } = string.Empty;
    public IEnumerable<PaymentPlanInclude> Inclusions { get; set; } = new List<PaymentPlanInclude>();
}
