namespace QuickHire.Application.Admin.Models.SubCategories;

public class SubCategoryRowModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string MainCategoryName { get; set; } = string.Empty;
    public int Clicks { get; set; }
    public int SubSubCategories { get; set; }
    public string CreatedOn { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
