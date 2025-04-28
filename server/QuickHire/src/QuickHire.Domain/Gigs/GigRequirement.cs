using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Gigs;

public class GigRequirement : BaseSoftDeletableEntity<int>
{ 
    public int GigId { get; set; }
    public Gig Gig { get; set; } = null!;
    public bool IsFileUpload { get; set; } 
    public string Question { get; set; } = string.Empty;
}
