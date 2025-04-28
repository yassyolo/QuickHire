using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Gigs;

public class Tag : BaseSoftDeletableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public int GigId { get; set; }
    public Gig Gig { get; set; } = null!;
}
