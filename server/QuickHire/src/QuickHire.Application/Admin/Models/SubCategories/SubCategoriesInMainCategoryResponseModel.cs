namespace QuickHire.Application.Admin.Models.SubCategories;

public class SubCategoriesInMainCategoryResponseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public IEnumerable<SubSubCategoriesInHeaderResponseModel> SubSubCategories { get; set; } = new List<SubSubCategoriesInHeaderResponseModel>();
}
