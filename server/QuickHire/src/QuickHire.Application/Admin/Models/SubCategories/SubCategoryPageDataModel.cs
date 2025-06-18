namespace QuickHire.Application.Admin.Models.SubCategories;

public class SubCategoryPageDataModel
{
    public int MainCategoryId { get; set; }
    public string MainCategoryName { get; set; } = string.Empty;
    public string MainCategoryDescription { get; set; } = string.Empty;
    public string SubCategoryName { get; set; } = string.Empty;
}
