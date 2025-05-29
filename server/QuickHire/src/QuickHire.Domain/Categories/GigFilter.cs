using QuickHire.Domain.Categories.Enums;
using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Categories;

public class GigFilter : BaseSoftDeletableEntity<int>
{
    public string? Title { get; set; }
    public GigFilterType Type { get; set; }
    public int? SubSubCategoryId { get; set; }
    public SubSubCategory? SubSubCategory { get; set; }
    public IEnumerable<FilterOption> Options { get; set; } = new List<FilterOption>();
}
