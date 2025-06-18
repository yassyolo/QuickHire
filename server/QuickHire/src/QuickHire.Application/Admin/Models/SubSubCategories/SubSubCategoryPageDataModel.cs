namespace QuickHire.Application.Admin.Models.SubSubCategories;

public class SubSubCategoryPageDataModel
{
    public int MainCategoryId { get; set; }
    public string MainCategoryName { get; set; } = string.Empty;
    public string MainCategoryDescription { get; set; } = string.Empty;
    public string SubSubCategoryName { get; set; } = string.Empty;
    public string SubCategoryName { get; set; } = string.Empty;
    public int SubCategoryId { get; set; }

}
