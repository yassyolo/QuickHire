namespace QuickHire.Application.Admin.Models.MainCategories;

public class MainCategoryDetailsModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Clicks { get; set; }
    public string CreatedOn { get; set; } = string.Empty;
    public IEnumerable<SubCategoriesInMainCategoryModel>? SubCategories { get; set; }
}
