using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Orders;

public class GigRequirementAnswer : BaseSoftDeletableEntity<int>
{
    public int GigRequirementId { get; set; }
    public GigRequirement GigRequirement { get; set; } = null!;
    public string Answer { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public string BuyerId { get; set; } = string.Empty;
}   

