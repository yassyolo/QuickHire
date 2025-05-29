namespace QuickHire.Application.Admin.Models.SubSubCategories;

public class SubSubCategoryForDeleteModel
{
    public int Id { get; set; }
    public int Gigs { get; set; }
    public IEnumerable<FilterForSubSubCategoryModel> SubSubCategoryFilters { get; set; } = new List<FilterForSubSubCategoryModel>();
}
