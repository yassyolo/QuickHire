using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Categories;

public class FAQ : BaseSoftDeletableEntity<int>
{
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int? MainCategoryId { get; set; }
    public MainCategory? MainCategory { get; set; }
    public int? GigId { get; set; }
    public Gig? Gig { get; set; }
    public string? UserId { get; set; }
}
