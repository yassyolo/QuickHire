namespace QuickHire.Application.Admin.Models.SubSubCategories;

public class SubSubCategoryRowModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Filters { get; set; }
    public int Clicks { get; set; }
    public string CreatedOn { get; set; } = string.Empty;
    public int Gigs { get; set; }
    public string SubCategoryName { get; set; } = string.Empty;
}
