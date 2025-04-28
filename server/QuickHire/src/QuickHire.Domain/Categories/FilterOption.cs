using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Categories;

public class FilterOption : BaseSoftDeletableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public GigFilter GigFilter { get; set; } = null!;
    public int GigFilterId { get; set; }
}
