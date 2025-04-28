using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Moderation;

public class ReportedItem : BaseSoftDeletableEntity<int>
{
    public int? GigId { get; set; }
    public Gig? Gig { get; set; }
    public string? ReportedUserId { get; set; }
    public string ReportedById { get; set; } = null!;
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } 
}
