namespace QuickHire.Application.Admin.Models.SubCategories;

public class GetSubCategoryForEditModel
{
    public int Id { get; set; }
    public int MainCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
