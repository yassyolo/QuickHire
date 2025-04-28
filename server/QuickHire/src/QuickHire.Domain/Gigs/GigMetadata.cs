using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Gigs;

public class GigMetadata : BaseSoftDeletableEntity<int>
{
    public int GigId { get; set; }
    public Gig Gig { get; set; } = null!;
    public int FilterOptionId { get; set; }
    public FilterOption FilterOption { get; set; } = null!;
}
