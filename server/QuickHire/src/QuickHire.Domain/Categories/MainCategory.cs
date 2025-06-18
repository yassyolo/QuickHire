using QuickHire.Domain.Shared.Implementations;

namespace QuickHire.Domain.Categories;

public class MainCategory : BaseSoftDeletableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Clicks { get; set; }
    public DateTime CreatedOn { get; set; } 
    public IEnumerable<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    public IEnumerable<FAQ> FAQs { get; set; } = new List<FAQ>();
}
