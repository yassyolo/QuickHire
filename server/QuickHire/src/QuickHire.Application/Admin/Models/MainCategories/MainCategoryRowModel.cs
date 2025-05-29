namespace QuickHire.Application.Admin.Models.MainCategories;

public class MainCategoryRowModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int SubCategories { get; set; }
    public int Clicks { get; set; }
    public string CreatedOn { get; set; } = string.Empty;
}
