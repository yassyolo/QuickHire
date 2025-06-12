using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Gigs;

public class PaymentPlanInclude : BaseSoftDeletableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int PaymentPlanId { get; set; }
    public PaymentPlan PaymentPlan { get; set; } = null!;
}
