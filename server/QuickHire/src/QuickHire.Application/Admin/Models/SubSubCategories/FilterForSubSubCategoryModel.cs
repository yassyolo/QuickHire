namespace QuickHire.Application.Admin.Models.SubSubCategories;

public class FilterForSubSubCategoryModel
{
    public int? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public IEnumerable<FilterOptionsForGigFilterModel> FilterOptions { get; set; } = new List<FilterOptionsForGigFilterModel>();
}
