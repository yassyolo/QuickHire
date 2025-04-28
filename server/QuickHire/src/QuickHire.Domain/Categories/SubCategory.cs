using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Categories;

public class SubCategory : BaseSoftDeletableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int MainCategoryId { get; set; }
    public MainCategory MainCategory { get; set; } = null!;
    public IEnumerable<SubSubCategory> SubSubCategories { get; set; } = new List<SubSubCategory>();
}

