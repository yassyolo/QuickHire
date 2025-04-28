using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Moderation;

public class DeactivatedRecord : BaseSoftDeletableEntity<int>
{
    public string? UserId { get; set; }
    public int? GigId { get; set; }
    public Gig? Gig { get; set; }
    public int? ReportedItemId { get; set; }
    public ReportedItem? ReportedItem { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
