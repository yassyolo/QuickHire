namespace QuickHire.Application.Admin.Models.SubSubCategories;

public class AddSubSubCategoryModel
{
    public string Name { get; set; } = string.Empty;
    public int SubCategoryId { get; set; }
    public IEnumerable<FilterForSubSubCategoryModel> Filters { get; set; } = new List<FilterForSubSubCategoryModel>();
}
