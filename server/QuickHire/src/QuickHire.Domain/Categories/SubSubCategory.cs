using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Categories;

public class SubSubCategory : BaseSoftDeletableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; } = null!;
    public IEnumerable<Gig>? Gigs { get; set; } 
    public IEnumerable<GigFilter> GigFilters { get; set; } = new List<GigFilter>();
}
